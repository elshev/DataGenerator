(function () {
    "use strict";

    angular
        .module("app")
        .directive("msgConfirmClick", msgConfirmClick);

    msgConfirmClick.$inject = ["$window"];

    function msgConfirmClick($window) {
        return {
            priority: -1,
            restrict: 'A',
            scope: { confirmFunction: "&msgConfirmClick" },
            link: function(scope, element, attrs) {
                element.bind('click',
                    function(e) {
                        var message = attrs.msgConfirmClickMessage ? attrs.msgConfirmClickMessage : "Are you sure?";
                        if ($window.confirm(message)) {
                            scope.confirmFunction();
                        }
                    });
            }
        };
    }
})();
