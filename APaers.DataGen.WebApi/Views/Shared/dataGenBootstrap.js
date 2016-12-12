"use strict";
(function () {

    var app = angular.module("app", ["ngRoute", "ui.bootstrap", "blockUI"]);

    app.config(["$controllerProvider", "$provide", function ($controllerProvider, $provide) {
        app.register =
          {
              controller: $controllerProvider.register,
              service: $provide.service
          };
    }]);

})();
