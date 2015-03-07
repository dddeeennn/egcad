/*table editor
need test and debug
*/

egcad.TableEditor = function (options, element) {
	var $elem = $(element),
		removeUrl = options.removeUrl,
		saveUrl = options.saveUrl,
			swapUrl = options.swapUrl;

	function init() {

	}

	function editRow(self) {
		var curEditor = $(document).find('#editor');

		var parent = null;

		if (curEditor.length) {
			parent = curEditor.closest('tr');
			var x = parseFloat(curEditor.find('.pointX input').val()) || 0;
			var paramValues = curEditor.find('.parameter input').map(function () {
				return parseFloat($(this).val());
			}).toArray();
			completeEdit(x, paramValues, parent.find('a').first().attr('id'), parent);
			container.on('editor-refresh', function () {
				setupEditor(null, "tr#" + self.closest('tr').attr('id'));
				container.off();
			});
		} else {
			setupEditor(self.closest('tr'));
		}
	}

	function removeRow(self) {
		var id = self.attr('id');
		sender.get("removeEntry", '@Url.Action("Remove")', { id: id }, function (response) {
			if (response && response.statusCode == 0) {
				self.closest('tr').empty();
				refreshState(response.data);
			} else {
				alert('Ошибка при удалении точки!');
			}
		});
	}


	function getValue($container, selector) {
		return parseFloat($container.find(selector).val());
	}

	function getValues($container, selector) {
		return $container.find(selector).map(function () {
			return parseFloat($(this).val()) || 0;
		}).toArray();
	}

	function setupEditor(parent, selector) {

		if (selector) {
			parent = $(selector, container);
		}
		$tbody.addClass('editor-mode');
		var $pointXContainer = parent.find('.pointX');
		if (IsSupportsHTML5Storage) {
			var pointXMin = localStorage['XMin'];
			var pointXMax = localStorage['XMax'];
			$pointXContainer.html(createEditor(parent, '.pointX', null,
			{
				validate: function (value) {
					return value.isBetween(pointXMin, pointXMax);
				},
				errorMessage: 'введите значение в диапазоне [' + pointXMin + ', ' + pointXMax + ']'
			}));
		} else {
			$pointXContainer.html(createEditor(parent, '.pointX'));
		}

		$.each(parent.find('.parameter'), function () {
			var self = $(this);
			self.html(createEditor(parent, '.parameter', self));
		});

		parent.find('a.edit-action').off()
									.on('click', function () {
										var x = getValue(parent, '.pointX input');
										var paramValues = getValues(parent, '.parameter input');
										completeEdit(x, paramValues, parent.find('a').first().attr('id'), parent);
										parent.attr('id', '');
									}).removeClass('glyphicon-edit')
									  .addClass('glyphicon-ok');
		parent.attr('id', 'editor');
	}

	function createEditor(parent, selector, obj, validator) {
		var oldValue = "";
		if (obj) {
			oldValue = obj.text();
		} else {
			oldValue = parent.find(selector).text();
		}

		var $editor = $("<div class='form-inline'/>");
		var textarea = $("<input type='text' class='form-control' style='margin-right:5px;max-width:90px;min-width: 30px; width: auto;height:20px;'>").val(oldValue);

		if (validator) {
			textarea.on('input', function () {
				var self = $(this);
				var value = self.val();
				if ($.isNumeric(value)) {
					if (!validator.validate(parseFloat(value))) {
						$editor.addClass('has-error');
						self.attr('title', validator.errorMessage);
						self.tooltip('show');
					} else {
						$editor.removeClass('has-error');
					}
				} else {
					$editor.addClass('has-error');
					self.attr('title', validator.errorMessage);
				}
			});
		}

		$editor.append(textarea);
		return $editor;
	}

	function completeEdit(pointX, paramValues, id, editorContainer) {
		$tbody.removeClass('editor-mode');

		editorContainer.find(".pointX").html(pointX);

		var newParameters = $tbody.find('.parameter input');
		$.each(paramValues, function (i, v) {
			$(newParameters[i]).closest('td.parameter').html(v);
		});
		saveHandler(pointX, paramValues, id);
		editorContainer.find('a.edit-action').off()
											.on('click', function () {
												editHandler($(this));
											}).removeClass('glyphicon-ok')
											  .addClass('glyphicon-edit');
	}

	function refreshState(state) {
		$tbody.loadTemplate($('#parameterEntryTmpl'), state.items);
		$tbody.find("a.edit-action").on("click", function () {
			editHandler($(this));
		});
		$tbody.find('tr').on('dblclick', function () {
			editHandler($(this).find('a.edit-action'));
		});
		$tbody.find("a.remove-action").on("click", function () {
			removeHandler($(this));
		});
		$tbody.find('tr').each(function () {
			var self = $(this);
			var idx = self[0].rowIndex;
			self.find('td.index').text(idx);
		});
		container.trigger('editor-refresh');

		localStorage.setItem('XMin', state.XMin);
		localStorage.setItem('XMax', state.XMax);

		validate();
	}

	function saveHandler(pointX, paramValues, id) {
		sender.get("SavePoint", '@Url.Action("Save")?x=' + pointX + '&' + 'id=' + id + prepareUrl(paramValues),
			{}, function (response) {
				if (response && response.statusCode == 0) {
					refreshState(response.data);
				} else {
					alert('Ошибка при сохранении параметра!');
				}
			}, null, {});
	};

	function swapHandler(ids) {
		sender.get("swap", '@Url.Action("Swap")?ids=' + ids.shift() + prepareIdsUrl(ids), {}, function (response) {
			if (response && response.statusCode == 0) {
				refreshState(response.data);
			} else {
				alert('Ошибка при сохранении параметра!');
			}
		}, null, {});
	};

	function prepareIdsUrl(array) {
		return array.map(function (v) {
			return '&ids=' + v;
		}).join('');
	}

	function prepareUrl(array) {
		return array.map(function (v) {
			return '&values=' + v;
		}).join('');
	}

	sender.get("loadState", '@Url.Action("GetState")', {}, function (response) {
		if (response && response.statusCode == 0) {
			refreshState(response.data);
		}
	});

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
		swapHandler(ids);
	};

	$("#sortable tbody").sortable({
		helper: fixHelperModified,
		stop: updateIndex,
		cancel: '.editor-mode'
	}).disableSelection();

	function destroy() {
	
	}

	init();

	this.destroy = destroy;
};
