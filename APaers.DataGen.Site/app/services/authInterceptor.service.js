(function () {
    "use strict";

    angular
        .module("app")
        .factory("authInterceptorService", authInterceptorService);

    authInterceptorService.$inject = ["$q", "$location", "localStorageService"];

    function authInterceptorService($q, $location, localStorageService) {
        var service = {
            request: request,
            responseError: responseError
        };
        return service;

        function request(config) {
            config.headers = config.headers || {};
            var userInfo = localStorageService.get("userInfoData");
            if (userInfo && userInfo.token) {
                config.headers.Authorization = "Bearer " + userInfo.token;
            }
            return config;
        }

        function responseError(rejection) {
            /*if (rejection.status === 401) {
                $location.path("/");
            }*/
            return $q.reject(rejection);
        }

    }
})();
