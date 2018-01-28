(function () {
    "use strict";

    angular
        .module("app")
        .controller("indexController", IndexController);

    IndexController.$inject = ["$location", "authService"];

    function IndexController($location, authService) {
        /* jshint validthis:true */
        var vm = this;
        vm.logOut = logOut;
        vm.copyrightYear = moment().year;
        vm.userInfo = authService.userInfo;

        activate();

        function activate() {
        }

        function logOut() {
            authService.logOut();
            $location.path("/home");
        }
    }
})();
