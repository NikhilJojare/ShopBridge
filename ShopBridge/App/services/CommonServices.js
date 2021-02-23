//Service to call Local Rest API
ShopBridgesApp.factory('CommonServices', ['$http', '$q', '$log', 'LocalServiceURL', 'ExceptionHandler', function ($http, $q, $log, LocalServiceURL, ExceptionHandler) {

    var CommonService = {};

    // PRIVATE ELEMENTES
    var headerAccept = "application/json;odata=verbose";
    var configuration = null;

    //Get request function
    function qhttp(req) {
        //debugger;
        var deferred = $q.defer();
        $http(req).then(function (data) {
            //console.log("request");
            if (data && data.d && data.d.results) {
                data.d.results.prevUrl = req.url;
                deferred.resolve(data.d.results);
            } else if (data && data.d) {
                data.d.prevUrl = req.url;
                deferred.resolve(data.d);
            } else {
                deferred.resolve(data);
            }
        }, function (error) {
            deferred.reject(error);
            //$log.error('DataService:' + req.url);
        });

        return deferred.promise;
    }


    function qhttpGet(url) {
        return qhttp({
            method: "GET",
            url: url,
            headers: { "Accept": "application/json;odata=verbose" }
        });
    }

    function getData(list) {
        var url;
        url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + list.title + "')/items?$select=" + list.fields.toString();

        if (list.hasOwnProperty('lookupFields') && list.lookupFields.toString().length > 0) {
            url += "&$expand=" + list.lookupFields.toString();
        }
        if (list.hasOwnProperty('filter') && list.filter && list.filter.toString().length > 0) {
            url += "&$filter=" + list.filter;
        }

        if (list.hasOwnProperty('limitTo') && list.limitTo > 0) {
            url += "&$top=" + list.limitTo.toString();
        }

        if (list.hasOwnProperty('orderBy') && list.orderBy) {
            url += "&$orderby=" + list.orderBy.toString();
        }
        //console.log('Just before executing..' +url);
        return qhttpGet(url);
    }


    CommonService.GetListItems = function (list) {
        var url;
        url = LocalServiceURL + "/odata/" + list.title + "?$select=" + list.fields.toString();

        if (list.hasOwnProperty('lookupFields') && list.lookupFields.toString().length > 0) {
            url += "&$expand=" + list.lookupFields.toString();
        }
        if (list.hasOwnProperty('filter') && list.filter && list.filter.toString().length > 0) {
            url += "&$filter=" + list.filter;
        }
        if (list.hasOwnProperty('limitTo') && list.limitTo > 0) {
            url += "&$top=" + list.limitTo.toString();
        }
        if (list.hasOwnProperty('orderBy') && list.orderBy) {
            url += "&$orderby=" + list.orderBy.toString();
        }
       

        return qhttpGet(url);
    }


    //Save data 
    CommonService.PostData = function (model, data) {
        //Prepare request object
        var req = {
            method: 'POST',
            cache: false,
            url: LocalServiceURL + '/odata/' + model,
            headers: {
                'Content-Type': 'application/json; odata=verbose'
            },
            data: data
        };
        //Call WCF Service
        var promise = $http(req).then(function (response) {
            return response.data;
        }, function (response) {
            //Exception Handling
            ExceptionHandler.HandleException(response);
        });
        return promise;
    };

    //Update Data
    CommonService.UpdateData = function (model, data, id) {
        //Prepare request object
        var req = {
            method: 'PATCH',
            cache: false,
            url: LocalServiceURL + '/odata/' + model + '/(' + id + ')',
            headers: {
                'Content-Type': 'application/json; odata=verbose'
            },
            data: data
        };
        //Call WCF Service
        var promise = $http(req).then(function (response) {
            return response.data;
        }, function (response) {
            //Exception Handling
            ExceptionHandler.HandleException(response);
        });
        return promise;
    };

    //Delete Data
    CommonService.DeleteData = function (model, id) {
        //Prepare request object
        var req = {
            method: 'DELETE',
            cache: false,
            url: LocalServiceURL + '/odata/' + model + '/(' + id + ')',
            headers: {
                'Content-Type': 'application/json; odata=verbose'
            },

        };
        //Call WCF Service
        var promise = $http(req).then(function (response) {
            return response.data;
        }, function (response) {
            //Exception Handling
            ExceptionHandler.HandleException(response);
        });
        return promise;
    };

    return CommonService;
}]);

