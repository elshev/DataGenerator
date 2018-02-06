(function () {
    "use strict";

    angular
        .module("app")
        .factory("libService", libService);

    libService.$inject = [];

    function libService() {
        var service = {
            processResponse: processResponse
        };
        return service;

        function processResponse(response, vm, errorPrefix) {
            var errors = [];
            if (!response) return "";
            switch (response.status) {
                case 400:
                    errorPrefix = errorPrefix ? errorPrefix : "Bad request due to";
                    errors.push(response.data.message);
                    break;
                case 404:
                    errorPrefix = errorPrefix ? errorPrefix : "Entity was not found";
                    errors.push(response.statusText);
                    break;
            }

            if (response.error_description)
                errors.push(response.error_description);

            if (response.data) {
                if (response.data.modelState) {
                    for (var key in response.data.modelState) {
                        for (var i = 0; i < response.data.modelState[key].length; i++) {
                            errors.push(response.data.modelState[key][i]);
                        }
                    }
                }
                else if (angular.isString(response.data))
                    errors.push(response.data);
            }

            var message = errors.join("; ");
            if (errorPrefix)
                message = errorPrefix + ": " + message;
            if (vm)
                vm.message = message;
            return message;
        }
    }
})();
