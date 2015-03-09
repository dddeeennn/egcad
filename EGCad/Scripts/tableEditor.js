/*
* object element            is selector or Jquery function contains table element
* object options            contains initialize properties 
* string rowTmplSelector    selector table row for render 
* string removeUrl          server remove row url
* string onRefresh          callback which exected after table refresh state
* string saveUrl            server save row url
* string refreshUrl         server refresh table url
* string swapUrl            server swap rows url
* bool   isSortable         enable table row reordering
*/
egcad.TableEditor = function (element, options) {
    var $elem = $(element),
        rowTmplSelector = options.rowTmplSelector,
        removeUrl = options.removeUrl,
        onRefreshCallback = options.onRefresh,
        saveUrl = options.saveUrl,
        refreshUrl = options.refreshUrl,
        isSortable = options.isSortable || false,
        swapUrl = options.swapUrl,
        $tbody = null,
        rowTmpl = null,
        sender = new egcad.Data.Sender();

    function init() {
        if (!$elem.is("table")) {
            $elem = $elem.find('table');
            if (!$elem.length) throw new Exception('argument "element" must be table  or contain table element ');
        }

        $tbody = $elem.find('tbody');

        if (rowTmplSelector) {
            rowTmpl = $(rowTmplSelector);
        } else {
            throw new Exception('rowTmplSelector is required parameter!');
        }

        refresh();

        if (isSortable) {
            $tbody.sortable({
                helper: fixHelperModified,
                stop: updateIndex,
                cancel: '.editor-mode'
            }).disableSelection();
        }
    }

    function editorHandler() {
        editRow($(this));
    }

    function editRow(self) {
        var curEditor = $elem.find('#editor');

        var parent = null;

        if (curEditor.length) {
            parent = curEditor.closest('tr');
            completeEdit(parent);
            $elem.on('tableEditor.stateRefreshed', function () {
                setupEditor($elem.find("tr#" + self.closest('tr').attr('id')));
                $elem.off();
            });
        } else {
            setupEditor(self.closest('tr'));
        }
    }

    function removeRow(id) {
        sender.get("removeRow", removeUrl, { id: id }, function (response) {
            if (response && response.statusCode == 0) {
                refreshState(response.data);
            } else {
                alert('Ошибка при удалении');
            }
        }, function () {
            alert('Ошибка при удалении');
        });
    }

    function setValues(cell, values) {
        cell.each(function () {
            var self = $(this);
            self.closest('td').html(values[self.data('name')]);
        });
    }

    function getValues(cells) {
        var values = [];
        cells.each(function () {
            var self = $(this);
            values[self.data('name')] = self.find('input').val();
        });
        return values;
    }

    function setupEditor(parent) {
        $tbody.addClass('editor-mode');

        parent.find('.editable').each(function () {
            var self = $(this);
            self.html(createCellEditor(self));
        });

        parent.find('a.edit-action').off()
									.on('click', function () {
									    completeEdit(parent);
									})
                                    .removeClass('glyphicon-edit')
									.addClass('glyphicon-ok');
        parent.attr('id', 'editor');
    }

    function createCellEditor(cell) {
        var oldValue = cell ? cell.text() : "";

        var $editor = $("<div class='form-inline'/>");
        var textarea = $("<input type='text' class='form-control editor-input'>").val(oldValue);

        $editor.append(textarea);
        return $editor;
    }

    function completeEdit(editorContainer) {
        var cellsToEdit = editorContainer.find('.editable');

        var values = getValues(cellsToEdit),
            id = editorContainer.find('a').first().attr('id');

        $tbody.removeClass('editor-mode');
        editorContainer.attr('id', '');

        setValues(cellsToEdit, values);

        saveRow(id, values);

        editorContainer.find('a.edit-action').off()
											 .on('click', editorHandler)
                                             .removeClass('glyphicon-ok')
											 .addClass('glyphicon-edit');
    }

    function refreshState(state) {
        $tbody.loadTemplate(rowTmpl, state.items);
        $tbody.find("a.edit-action").on(' click', editorHandler);
        $tbody.find(".editable").on('dblclick', editorHandler);
        $tbody.find("a.remove-action").on("click", function () {
            removeRow($(this).attr('id'));
        });
        $tbody.find('tr').each(function () {
            var self = $(this);
            var idx = self[0].rowIndex;
            self.find('td.index').text(idx);
        });
        if (onRefreshCallback) onRefreshCallback();
        $elem.trigger('tableEditor.stateRefreshed');
    }

    function saveRow(id, values) {
        sender.post("SaveRow", saveUrl, $.extend({ id: id }, values),
            function (response) {
                if (response && response.statusCode == 0) {
                    refreshState(response.data);
                } else {
                    alert('Ошибка при сохранении параметров!');
                }
            });
    };

    function swapRow(ids) {
        sender.post("SwapRow", swapUrl, { ids: ids }, function (response) {
            if (response && response.statusCode == 0) {
                refreshState(response.data);
            } else {
                alert('Ошибка при обновлении таблицы');
            }
        }, null, {});
    };

    var fixHelperModified = function (e, tr) {
        var $originals = tr.children();
        var $helper = tr.clone();
        $helper.children().each(function (index) {
            $(this).width($originals.eq(index).width());
        });
        return $helper;
    };
    var updateIndex = function () {
        var ids = $tbody.find('tr').map(function () {
            return parseInt($(this).attr('id'));
        }).toArray();
        swapRow(ids);
    };

    function destroy() {
        $elem.off();

        $tbody = null;
        $elem = null;
    }

    function refresh() {
        sender.get("load", refreshUrl, {}, function (response) {
            if (response && response.statusCode == 0) {
                refreshState(response.data);
            }
        });
    }

    function isEmpty() {
        return $tbody.find('tr').length > 1;
    }

    init();

    this.isEmpty = isEmpty;
    this.refresh = refresh;
    this.destroy = destroy;
};
