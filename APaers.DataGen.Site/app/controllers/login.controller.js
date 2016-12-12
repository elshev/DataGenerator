(function () {
    "use strict";

    angular
        .module("app")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$location", "authService", "libService"];

    function LoginController($location, authService, libService) {
        /* jshint validthis:true */
        var vm = this;
        vm.loginData = { };
        vm.message = "";
        vm.login = login;
        vm.savedSuccessfully = false;
        vm.registration = {};
        vm.register = register;

        activate();

        function activate() {
            clearRegistration();
        }

        function clearRegistration() {
            vm.registration.userName = "";
            vm.registration.email = "";
            vm.registration.password = "";
            vm.registration.confirmPassword = "";
        }

        function login() {
            authService.login(vm.loginData)
                .then(function (response) {
                    $location.path("/");
                },
                    function (response) {
                        libService.processResponse(response, vm);
                    });
        };

        function register() {
            authService.saveRegistration(vm.registration, registerSuccess, registerError);
        };

        function registerSuccess(data) {
            vm.loginData.userName = vm.registration.userName;
            vm.loginData.email = vm.registration.email;
            vm.loginData.password = vm.registration.password;
            clearRegistration();
            vm.login();
        }

        function registerError(response) {
            libService.processResponse(response, vm, "Failed to register user due to");
        }
    };
})();
