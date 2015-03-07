"use strict";

egcad.Data = egcad.Data || {};

/**
* fix ui notifier
 * @class {egcad.Data.Sender}  data provider 
 */
egcad.Data.Sender = function () {

	// singleton
	if (egcad.Data.Sender.__instance) return egcad.Data.Sender.__instance;
	egcad.Data.Sender.__instance = this;
	/**
	 * @type {egcad.Data.Provider|null}
	 */
	var dataProvider = null;

	var notifierItems = {};

	/**
	 *
	 * @type {egcad.UI.Notifier|null}
	 */
	var uiNotifier = null;

	var providerChangeTimeout = null;

	function init() {
		if (dataProvider) return;

		dataProvider = new egcad.Data.Provider();
		$(dataProvider).bind(egcad.Data.Provider.Event.CHANGE, dataProviderChangeHandler);
	}

	function dataProviderChangeHandler() {
		// prevent frequent updates
		if (providerChangeTimeout) clearTimeout(providerChangeTimeout);
		providerChangeTimeout = setTimeout(updateNotifier, 100);
	}


	function updateNotifier() {
		var errors = dataProvider.getErrors();

		if (uiNotifier == null && $.isEmptyObject(errors)) return;
		uiNotifier = uiNotifier || new egcad.UI.Notifier();

		var errorIndexes = {};
		$.each(errors, function (requestName, error) {
			errorIndexes[getErrorIndex(error)] = true;
		});

		// removes not actual messages
		$.each(notifierItems, function (index, notifierItem) {
			if (errorIndexes[index]) return true;
			notifierItem.destroy();
		});

		// add new messages
		$.each(errors, function (requestName, error) {
			var index = getErrorIndex(error);
			var notifierItem = notifierItems[index];
			if (notifierItem) return true;

			notifierItem = createNotifierItem(error);
			uiNotifier.addMessage(notifierItem);
			notifierItems[index] = notifierItem;
		});
	}

	/**
	 * @param {egcad.Data.Request.Error} error
	 * @returns {egcad.UI.Notifier.Item}
	 */
	function createNotifierItem(error) {
		var actions = {};

		if (error.canRetry()) actions['tryAgain'] = 'Попробовать еще';
		actions['close'] = '✖';

		var item = new egcad.UI.Notifier.Item(error.getDescription(), actions, egcad.UI.Notifier.Item.Type.ERROR);
		$(item)
			.on(egcad.UI.Notifier.Item.Event.DESTROY, function (e, actionName) {
				delete notifierItems[getErrorIndex(error)];
			})
			.on(egcad.UI.Notifier.Item.Event.ACTION, function (e, actionName) {
				switch (actionName) {
					case 'close':
						$.each(getRequestNames(error), function (index, requestName) {
							dataProvider.destroyRequest(requestName);
						});
						item.destroy();
						break;

					case 'tryAgain':
						$.each(getRequestNames(error), function (index, requestName) {
							dataProvider.resend(requestName);
						});
						break;
				}
			});

		return item;
	}

	/**
	 * Returns index string associated with error
	 * @param {egcad.Data.Request.Error} error
	 * @returns {String}
	 */
	function getErrorIndex(error) {
		return error.getDescription();
	}

	/**
	 * Returns an array of request names by error given error description
	 * @param {WLFlow.Data.Request.Error} error
	 * @returns {Array.<String>}
	 */
	function getRequestNames(error) {
		var errors = dataProvider.getErrors();

		var result = [];
		var errorIndex = getErrorIndex(error);
		$.each(errors, function (requestName, error) {
			var index = getErrorIndex(error);
			if (errorIndex == index) result.push(requestName);
		});

		return result;
	}


	function getAjaxOptions(methodType, url, params, onSuccess, onError) {
		var options = {
			type: methodType,
			// dataType: 'json',
			url: url
		};

		if (params) options.data = params;
		if ($.isFunction(onSuccess)) options.success = onSuccess;
		if ($.isFunction(onError)) options.error = onError;

		return options;
	}

	function getAjaxOptionsWithJsonParams(methodType, url, params, onSuccess, onError) {
		return getAjaxOptions(
			methodType,
			url,
			params ? JSON.stringify(params) : null,
			onSuccess,
			onError
		);
	}

	/**
	 * Request data from server
	 */
	function ajax(name, options) {
		init();

		return dataProvider.request(name, options);
	}

	/**
	 * @description GET request with result in standard format { statusCode: x, statusMessage: '...', data: {...}}
	 * @param {string} name  - request name
	 * @param {string} url - source url
	 * @param {object} queryParams - query parameters
	 * @param {Function} [onSuccess] - handler for result processing
	 * @param {Function} [onError] - handler for an error event
	 * @param {object} [options] - extra options for request (see jQuery.ajax)
	 * @returns {null}
	 */
	function get(name, url, queryParams, onSuccess, onError, options) {
		return ajax(
			name,
			$.extend(
				getAjaxOptions('GET', url, queryParams, onSuccess, onError),
				{ dataType: 'json' },
				options
			)
		);
	}

	/**
	 * @description POST request with result in standard format { statusCode: x, statusMessage: '...', data: {...}}
	 * @param {string} name  - request name
	 * @param {string} url - source url
	 * @param {object} queryParams - query parameters
	 * @param {Function} [onSuccess] - handler for result processing
	 * @param {Function} [onError] - handler for an error event
	 * @param {object} [options] - extra options for request (see jQuery.ajax)
	 * @returns {null}
	 */
	function post(name, url, queryParams, onSuccess, onError, options) {
		return ajax(
			name,
			$.extend(
				getAjaxOptions('POST', url, queryParams, onSuccess, onError),
				{ dataType: 'json' },
				options
			)
		);
	}

	// post|get methods
	// parameters data should be serialized in request Stream, not as query parameters
	function postJson(name, url, data, onSuccess, onError, ajaxOptions) {
		return ajax(
			name,
			$.extend(
				getAjaxOptionsWithJsonParams('POST', url, data, onSuccess, onError),
				{ contentType: 'application/json; charset=utf-8' },
				{ dataType: 'json' },
				ajaxOptions
			)
		);
	}

	function getJson(name, url, data, onSuccess, onError, ajaxOptions) {
		return ajax(
			name,
			$.extend(
				getAjaxOptionsWithJsonParams('GET', url, data, onSuccess, onError),
				{ contentType: 'application/json; charset=utf-8' },
				{ dataType: 'json' },
				ajaxOptions
			)
		);
	}

	this.get = get;
	this.post = post;
	this.postJson = postJson;
	this.getJson = getJson;
};
