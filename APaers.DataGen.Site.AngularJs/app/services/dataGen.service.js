(function() {
    "use strict";

    angular
        .module("app")
        .factory("dataGenService", dataGenService);

    dataGenService.$inject = ["ajaxService"];

    function dataGenService(ajaxService) {
        var service = {
            getGeneratedData: getGeneratedData,
            getTableInfo: getTableInfo
        };
        
        return service;


        function getTableInfo(script, sqlType, successFunction, errorFunction) {
            var scriptVm = { createTableScript: script, sqlType: sqlType};
            return ajaxService.post("GetTableInfo", scriptVm,
                function (tableInfoVm) {
                    var tableInfo = processTableInfo(tableInfoVm);
                    successFunction(tableInfo);
                }, errorFunction);
        }

        function processTableInfo(tableInfo) {
            if (tableInfo && tableInfo.columns) {
                angular.forEach(tableInfo.columns,
                    function (columnInfo) {
                        if (columnInfo.columnType === Enums.columnType.DateTime) {
                            if (angular.isString(columnInfo.minDateTime))
                                columnInfo.minDateTime = new Date(columnInfo.minDateTime);
                            if (angular.isString(columnInfo.maxDateTime))
                                columnInfo.maxDateTime = new Date(columnInfo.maxDateTime);
                        }
                    });
            }
            return tableInfo;
        }

        function getGeneratedData(tableInfoVm, successFunction, errorFunction) {
            ajaxService.post("GetGeneratedData", tableInfoVm, successFunction, errorFunction);
        }
    }
})();
