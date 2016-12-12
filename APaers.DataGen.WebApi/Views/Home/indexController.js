(function() {
    "use strict";

    angular
        .module("app")
        .controller("indexController", IndexController);

    IndexController.$inject = ["ajaxService"];


    function IndexController(ajaxService) {
        var vm = this;
        // enums from server
        vm.sqlTypes = sqlTypesEnum;
        vm.columnTypes = columnTypeEnum;
        vm.columnTypeInfos = columnTypeEnumInfo;
        vm.fullNameFormats = fullNameFormatEnum;
        vm.fullNameFormatInfos = fullNameFormatEnumInfo;
        //
        vm.sqlType = vm.sqlTypes.SqlServer;
        vm.entityCount = 3;
        vm.generatedText = "";
        vm.createTableInfo = createTableInfo;
        vm.deleteColumn = deleteColumn;
        vm.generateResult = generateResult;
        vm.getSettingsView = getSettingsView;

        activate();

        function activate() {
            vm.script = "create table dbo.Test02 (" +
                "Id int identity(1,1) not null, " +
                "SomeUniqueId uniqueidentifier not null, " +
                "SomeGuid varchar(64) not null, " +
                "Country varchar(128) not null, " +
                "RegionName nvarchar(128) not null, " +
                "City nvarchar(128) not null, " +
                "AddressLine1 varchar(256) not null, " +
                "AddressLine2 varchar(256) not null, " +
                "LatitudeLongitude varchar(32) not null, " +
                "SomeTextColumn varchar(128) not null, " +
                "PersonFullName varchar(16) not null, " +
                "Email varchar(64) not null, " +
                "Telephone varchar(16) not null, " +
                "PassportNo varchar(16) not null, " +
                "PostalCode varchar(12) not null, " +
                "WholeValue int not null, " +
                "DecimalValue decimal not null, " +
                "FloatValue float not null, " +
                "BoolValue bit not null, " +
                "DateTimeValue datetime not null, " +
                ");";
            vm.createTableInfo();
        }

        function getSettingsView(columnType) {
            switch (columnType) {
            case vm.columnTypes.Autoinc:
                return "/views/columnSettings/autoincSettings.html";
            case vm.columnTypes.RandomText:
                return "/views/columnSettings/randomTextSettings.html";
            case vm.columnTypes.FirstName:
            case vm.columnTypes.LastName:
                return "/views/columnSettings/personNameSettings.html";
            case vm.columnTypes.FullName:
                return "/views/columnSettings/fullNameSettings.html";
            case vm.columnTypes.Email:
                return "/views/columnSettings/emailSettings.html";
            case vm.columnTypes.PassportNumber:
                return "/views/columnSettings/passportNumberSettings.html";
            case vm.columnTypes.PostalCode:
                return "/views/columnSettings/postalCodeSettings.html";
            case vm.columnTypes.AddressLine1:
                return "/views/columnSettings/addressLine1Settings.html";
            case vm.columnTypes.AddressLine2:
                return "/views/columnSettings/addressLine2Settings.html";
            case vm.columnTypes.FullAddress:
                return "/views/columnSettings/fullAddressSettings.html";
            case vm.columnTypes.Int:
                return "/views/columnSettings/integerSettings.html";
            case vm.columnTypes.Number:
                return "/views/columnSettings/numberSettings.html";
            case vm.columnTypes.Money:
                return "/views/columnSettings/moneySettings.html";
            case vm.columnTypes.DateTime:
                return "/views/columnSettings/dateTimeSettings.html";
            default:
                return null;
            }
        }

        function createTableInfo() {
            var scriptVm = new Object();

            scriptVm.createTableScript = vm.script;
            scriptVm.sqlType = vm.sqlType;

            ajaxService.post("GetTableInfo", scriptVm, createTableInfoSuccess, createTableInfoError);
        }

        function createTableInfoSuccess(tableInfoVm) {
            var tableInfo = tableInfoVm.tableInfo;
            if (tableInfo && tableInfo.columns) {
                angular.forEach(tableInfo.columns,
                    function(columnInfo) {
                        if (columnInfo.columnType === vm.columnTypes.DateTime) {
                            if (angular.isString(columnInfo.minDateTime))
                                columnInfo.minDateTime = new Date(columnInfo.minDateTime);
                            if (angular.isString(columnInfo.maxDateTime))
                                columnInfo.maxDateTime = new Date(columnInfo.maxDateTime);
                        }

                    });
            }
            vm.tableInfo = tableInfoVm.tableInfo;
            vm.generateResult();
        }

        function createTableInfoError(error) {
            vm.tableInfo = {};
        }

        function deleteColumn(columnIndex) {
            vm.tableInfo.columns.splice(columnIndex, 1);
        }

        function generateResult() {
            var tableInfoVm = new Object();
            tableInfoVm.sqlType = vm.sqlType;
            tableInfoVm.entityCount = vm.entityCount;
            tableInfoVm.tableInfo = vm.tableInfo;
            tableInfoVm.columns = [{ columnId: 1 }, { columnId: 2 }];
            ajaxService.post("GetGeneratedData", tableInfoVm, generateResultSuccess, generateResultError);
        }

        function generateResultSuccess(generatedDataVm) {
            vm.generatedText = generatedDataVm.generatedData;
        }

        function generateResultError(error) {
            vm.generatedScript = "";
        }
    }
})();
