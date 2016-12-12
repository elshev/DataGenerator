(function () {
    "use strict";

    angular
        .module("app")
        .service("ajaxService", ajaxService);

    ajaxService.$inject = ["$http", "blockUI", "apiUrl"];

    function ajaxService($http, blockUi, apiUrl) {
        var vm = this;
        vm.get = get;
        vm.post = post;
        vm.postParams = postParams;
        vm.put = put;
        vm.remove = remove;


        function getUrl(route) {
            return apiUrl + route;
        }

        function http(method, route, params, data, successFunction, errorFunction) {
            blockUi.start();
            var url = getUrl(route);
            $http({
                url: url,
                method: method,
                params: params,
                data: data
            })
                .then(
                    function (response) {
                        blockUi.stop();
                        if (successFunction)
                            return successFunction(response.data);
                    },
                    function (response) {
                        blockUi.stop();
                        if (errorFunction)
                            return errorFunction(response);
                    });
        }

        function get(route, params, successFunction, errorFunction) {
            http("GET", route, params, null, successFunction, errorFunction);
        }

        function post(route, data, successFunction, errorFunction) {
            http("POST", route, null, data, successFunction, errorFunction);
        }

        function postParams(route, params, successFunction, errorFunction) {
            http("POST", route, params, null, successFunction, errorFunction);
        }

        function put(route, params, data, successFunction, errorFunction) {
            http("PUT", route, params, data, successFunction, errorFunction);
        }

        function remove(route, params, successFunction, errorFunction) {
            http("DELETE", route, params, null, successFunction, errorFunction);
        }
    }
})();
