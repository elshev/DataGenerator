(function () {
    "use strict";

    angular
        .module("app")
        .controller("homeController", HomeController);

    HomeController.$inject = ["dataGenService", "libService"];


    function HomeController(dataGenService, libService) {
        var vm = this;
        vm.columnTypeInfos = Enums.columnTypeInfo;
        vm.fullNameFormatInfos = Enums.fullNameFormatInfo;
        //
        vm.metaScriptMessage = null;
        vm.metaDataMessage = null;
        vm.sqlType = Enums.sqlType.SqlServer;
        vm.entityCount = 3;
        vm.generatedText = "";
        vm.getTableInfo = getTableInfo;
        vm.deleteColumn = deleteColumn;
        vm.getGeneratedData = getGeneratedData;
        vm.getSettingsView = getSettingsView;

        activate();

        function activate() {
            vm.script = "create table dbo.Test02 (" +
                "Id int identity(1,1) not null, " +
                "Country varchar(128) not null, " +
                "RegionName nvarchar(128) not null, " +
                "Email varchar(64) not null, " +
                "WholeValue int not null, " +
                "DateTimeValue datetime not null " +
                ");";

            /*vm.script = "create table dbo.Test02 (" +
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
                ");";*/
            vm.getTableInfo();
        }

        function getSettingsView(columnType) {
            var result = "app/views/columnSettings/";
            switch (columnType) {
                case Enums.columnType.Autoinc:
                    return result + "autoincSettings.html";
                case Enums.columnType.RandomText:
                    return result + "randomTextSettings.html";
                case Enums.columnType.FirstName:
                case Enums.columnType.LastName:
                    return result + "personNameSettings.html";
                case Enums.columnType.FullName:
                    return result + "fullNameSettings.html";
                case Enums.columnType.Email:
                    return result + "emailSettings.html";
                case Enums.columnType.PassportNumber:
                    return result + "passportNumberSettings.html";
                case Enums.columnType.PostalCode:
                    return result + "postalCodeSettings.html";
                case Enums.columnType.AddressLine1:
                    return result + "addressLine1Settings.html";
                case Enums.columnType.AddressLine2:
                    return result + "addressLine2Settings.html";
                case Enums.columnType.FullAddress:
                    return result + "fullAddressSettings.html";
                case Enums.columnType.Int:
                    return result + "integerSettings.html";
                case Enums.columnType.Number:
                    return result + "numberSettings.html";
                case Enums.columnType.Money:
                    return result + "moneySettings.html";
                case Enums.columnType.DateTime:
                    return result + "dateTimeSettings.html";
                default:
                    return null;
            }
        }

        function getTableInfo() {
            dataGenService.getTableInfo(vm.script, vm.sqlType, getTableInfoSuccess, getTableInfoError);
        }

        function getTableInfoSuccess(tableInfo) {
            vm.tableInfo = tableInfo;
            vm.metaScriptMessage = null;
            vm.getGeneratedData();
        }

        function getTableInfoError(response) {
            vm.tableInfo = {};
            vm.metaScriptMessage = libService.processResponse(response);
        }

        function deleteColumn(columnIndex) {
            vm.tableInfo.columns.splice(columnIndex, 1);
        }

        function getGeneratedData() {
            var tableInfoVm = {
                sqlType: vm.sqlType,
                entityCount: vm.entityCount,
                name: vm.tableInfo.name,
                columns: vm.tableInfo.columns
            }
            dataGenService.getGeneratedData(tableInfoVm, generateResultSuccess, generateResultError);
        }

        function generateResultSuccess(generatedDataVm) {
            vm.generatedText = generatedDataVm.generatedData;
            vm.metaDataMessage = null;
        }

        function generateResultError(response) {
            vm.generatedText = "";
            vm.metaDataMessage = libService.processResponse(response);
        }
    }
})();
