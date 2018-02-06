(function () {
    "use strict";

    angular
        .module("app")
        .factory("authService", authService);

    authService.$inject = ["$http", "$q", "localStorageService", "authUrl", "ajaxService"];

    function authService($http, $q, localStorageService, authUrl, ajaxService) {
        var userInfoStorageName = "userInfoData";
        var userInfo = {};

        clearUserInfo();

        var service = {
            saveRegistration: saveRegistration,
            login: login,
            logOut: logOut,
            fillUserInfo: fillUserInfo,
            userInfo: userInfo
        };
        return service;


        function clearUserInfo() {
            userInfo.isAuth = false;
            userInfo.userName = "";
            userInfo.isUser = false;
            userInfo.isManager = false;
            userInfo.isAdmin = false;
        }

        function logOut() {
            localStorageService.remove(userInfoStorageName);
            clearUserInfo();
        }

        function saveRegistration(registration, successFunction, errorFunction) {
            logOut();
            return ajaxService.post("account/register", registration, successFunction, errorFunction);
        }

        function login(loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();
            $http.post(authUrl + "token", data, { headers: { 'Content-Type': "application/x-www-form-urlencoded" } })
                .success(function(response) {
                    localStorageService.set(userInfoStorageName,
                    {
                        token: response.access_token,
                        userName: loginData.userName,
                        isUser: response.isUser,
                        isManager: response.isManager,
                        isAdmin: response.isAdmin
                    });
                    fillUserInfo();
                    deferred.resolve(response);
                })
                .error(function(err, status) {
                    logOut();
                    deferred.reject(err);
                });
            return deferred.promise;
        }

        function fillUserInfo() {
            var userInfoData = localStorageService.get(userInfoStorageName);
            if (userInfoData) {
                userInfo.isAuth = true;
                userInfo.userName = userInfoData.userName;
                userInfo.isUser = userInfoData.isUser;
                userInfo.isManager = userInfoData.isManager;
                userInfo.isAdmin = userInfoData.isAdmin;
            }
        }
    }
})();
