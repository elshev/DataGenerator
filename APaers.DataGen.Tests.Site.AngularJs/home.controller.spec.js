/// <reference path="_references.tests.js" />
describe("homeController",
    function () {
        "use strict";
        beforeEach(module("app"));

        var isErrorWhenGetTableInfo = false;
        var isErrorWhenGetGeneratedData = false;
        var testErrorMessage = "Test error message";
        var tableInfo = {name: "Test table",
            columns: [
                { "columnType": Enums.columnType.Autoinc, "startValue": 1, "incrementValue": 1, "systemColumnType": 2, "name": "Id", "columnId": 1, "maxLength": 4, "maxPrecision": 10, "scale": 0, "isNullable": false, "isIdentity": true, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.FullName, "fullNameFormat": 0, "systemColumnType": 1, "name": "PersonFullName", "columnId": 2, "maxLength": 16, "maxPrecision": 0, "scale": 0, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.Email, "systemColumnType": 1, "name": "Email", "columnId": 3, "maxLength": 64, "maxPrecision": 0, "scale": 0, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.Int, "min": 0, "max": 100000, "systemColumnType": 2, "name": "WholeValue", "columnId": 4, "maxLength": 4, "maxPrecision": 10, "scale": 0, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.DateTime, "minDateTime": "2011-12-01T00:00:00+04:00", "maxDateTime": "2021-12-01T00:00:00+03:00", "systemColumnType": 5, "name": "DateTimeValue", "columnId": 5, "maxLength": 8, "maxPrecision": 23, "scale": 3, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 }
            ]};
        var generatedDataVm = { generatedData: "Some generated script"};
        var dataGenServiceMock = null;

        beforeEach(function () {
            //ReSharperReporter.prototype.jasmineDone = function () { };
            isErrorWhenGetTableInfo = false;
            isErrorWhenGetGeneratedData = false;
            module(function ($provide) {
                $provide.value("dataGenService", dataGenServiceMock);
            });

            dataGenServiceMock = {
                getTableInfo: jasmine.createSpy("getTableInfo").and.callFake(
                    function (userId, filter, success, error) {
                        if (isErrorWhenGetTableInfo)
                            error({ status: 400, data: { message: testErrorMessage } });
                        else
                            success(tableInfo);
                    }),
                getGeneratedData: jasmine.createSpy("getGeneratedData").and.callFake(
                    function (tableInfoVm, success, error) {
                        if (isErrorWhenGetGeneratedData)
                            error({ status: 400, data: { message: testErrorMessage } });
                        else
                            success(generatedDataVm);
                    })
            };
        });
        var vm;
        beforeEach(inject(function ($controller, dataGenService) {
            vm = $controller("homeController",
            {
                dataGenService: dataGenService
            });
        }));

        it("homeController initialization",
            function () {
                expect(vm.tableInfo.length).toBe(tableInfo.length);
                expect(vm.generatedText).toEqual(generatedDataVm.generatedData);
            });

        it("getTableInfo(): vm.tableInfo should be empty when error",
            function () {
                // Arrange
                isErrorWhenGetTableInfo = true;
                // Act
                vm.getTableInfo();
                // Assert
                expect(dataGenServiceMock.getTableInfo).toHaveBeenCalled();
                expect(dataGenServiceMock.getTableInfo.calls.count()).toBe(2);
                expect(vm.tableInfo).toEqual({});
            });

        it("getTableInfo(): vm.tableInfo should contain columns",
            function () {
                // Arrange
                isErrorWhenGetTableInfo = false;
                // Act
                vm.getTableInfo();
                // Assert
                expect(dataGenServiceMock.getTableInfo).toHaveBeenCalled();
                expect(dataGenServiceMock.getTableInfo.calls.count()).toBe(2);
                expect(vm.tableInfo).toEqual(tableInfo);
            });

        it("deleteColumn(): test deleting column",
            function () {
                // Arrange
                var index = 2;
                var clonedTableInfo = JSON.parse(JSON.stringify(tableInfo));
                vm.tableInfo = tableInfo;
                // Act
                vm.deleteColumn(index);
                // Assert
                expect(vm.tableInfo.columns.length).toEqual(clonedTableInfo.columns.length - 1);
                expect(vm.tableInfo.columns[index].name).not.toEqual(clonedTableInfo.columns[index].name);
            });

        it("getGeneratedData(): vm.generatedText should be empty when error",
            function () {
                // Arrange
                isErrorWhenGetGeneratedData = true;
                // Act
                vm.getGeneratedData();
                // Assert
                expect(dataGenServiceMock.getGeneratedData).toHaveBeenCalled();
                expect(dataGenServiceMock.getGeneratedData.calls.count()).toBe(2);
                expect(vm.generatedText).toEqual("");
            });

        it("getGeneratedData(): vm.generatedText should contain generated text",
            function () {
                // Arrange
                isErrorWhenGetGeneratedData = false;
                var entityCount = 10;
                vm.sqlType = Enums.sqlType.SqlServer;
                vm.entityCount = entityCount;
                vm.tableInfo = tableInfo;
                var tableInfoVm = {
                    sqlType: vm.sqlType,
                    entityCount: vm.entityCount,
                    name: vm.tableInfo.name,
                    columns: vm.tableInfo.columns
                }

                // Act
                vm.getGeneratedData();
                // Assert
                expect(dataGenServiceMock.getGeneratedData).toHaveBeenCalledWith(tableInfoVm, jasmine.any(Function), jasmine.any(Function));
                expect(dataGenServiceMock.getGeneratedData.calls.count()).toBe(2);
                expect(vm.generatedText).toEqual(generatedDataVm.generatedData);
            });

    });
