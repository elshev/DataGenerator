/// <reference path="_references.tests.js" />
describe("dataGenService",
    function () {
        "use strict";
        beforeEach(module("app"));

        var isErrorWhenPost = false;
        var testErrorMessage = "Test error message";
        var tableInfo = {
            name: "Test table",
            columns: [
                { "columnType": Enums.columnType.Autoinc, "startValue": 1, "incrementValue": 1, "systemColumnType": 2, "name": "Id", "columnId": 1, "maxLength": 4, "maxPrecision": 10, "scale": 0, "isNullable": false, "isIdentity": true, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.FullName, "fullNameFormat": 0, "systemColumnType": 1, "name": "PersonFullName", "columnId": 2, "maxLength": 16, "maxPrecision": 0, "scale": 0, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.Email, "systemColumnType": 1, "name": "Email", "columnId": 3, "maxLength": 64, "maxPrecision": 0, "scale": 0, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.Int, "min": 0, "max": 100000, "systemColumnType": 2, "name": "WholeValue", "columnId": 4, "maxLength": 4, "maxPrecision": 10, "scale": 0, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 },
                { "columnType": Enums.columnType.DateTime, "minDateTime": "2011-12-01T00:00:00+04:00", "maxDateTime": "2021-12-01T00:00:00+03:00", "systemColumnType": 5, "name": "DateTimeValue", "columnId": 5, "maxLength": 8, "maxPrecision": 23, "scale": 3, "isNullable": false, "isIdentity": false, "isComputed": false, "nullPercent": 33, "defaultFormat": null, "format": null, "precision": 0 }
            ]};
        var generatedDataVm = { generatedData: "Some generated script"};
        var postRouteToResult = { "GetTableInfo": tableInfo, "GetGeneratedData": generatedDataVm };
        var ajaxServiceMock = null;
        var service;

        beforeEach(function () {
            //ReSharperReporter.prototype.jasmineDone = function () { };
            isErrorWhenPost = false;
            ajaxServiceMock = {
                post: jasmine.createSpy("post").and.callFake(
                    function (route, data, success, error) {
                        if (isErrorWhenPost)
                            error({ status: 400, data: { message: testErrorMessage } });
                        else
                            success(postRouteToResult[route]);
                    })
            };
            module(function ($provide) {
                $provide.value("ajaxService", ajaxServiceMock);
            });
        });

        beforeEach(inject(function (dataGenService) {
            service = dataGenService;
        }));

        it("getTableInfo(): ajaxService.post('GetTableInfo', ...) should be called",
            function () {
                // Arrange
                var script = "create table Test(id int not null);";
                var sqlType = Enums.sqlType.SqlServer;
                var responseData = null;
                var getTableInfoSuccess = function (data) {
                    responseData = data;
                };
                var errorSpy = jasmine.createSpy();
                // Act
                service.getTableInfo(script, sqlType, getTableInfoSuccess, errorSpy);
                // Assert
                expect(ajaxServiceMock.post).toHaveBeenCalledWith("GetTableInfo", { createTableScript: script, sqlType: sqlType }, jasmine.any(Function), errorSpy);
                expect(errorSpy).not.toHaveBeenCalled();
                expect(responseData).toEqual(tableInfo);
                for (var i = 0; i < responseData.columns.length; i++) {
                    var col = responseData.columns[i];
                    if (col.columnType === Enums.columnType.DateTime) {
                        expect(col.minDateTime instanceof Date).toEqual(true);
                        expect(col.maxDateTime instanceof Date).toEqual(true);
                    }
                }
                    
            });

        it("getTableInfo(): ajaxService.post('GetTableInfo', ...) should call error function",
            function () {
                // Arrange
                isErrorWhenPost = true;
                var script = "create table Test(id int not null);";
                var sqlType = Enums.sqlType.SqlServer;
                var successSpy = jasmine.createSpy();
                var errorSpy = jasmine.createSpy();
                // Act
                service.getTableInfo(script, sqlType, successSpy, errorSpy);
                // Assert
                expect(ajaxServiceMock.post).toHaveBeenCalledWith("GetTableInfo", { createTableScript: script, sqlType: sqlType }, jasmine.any(Function), errorSpy);
                expect(successSpy).not.toHaveBeenCalled();
                expect(errorSpy).toHaveBeenCalled();
            });

        it("getGeneratedData(): ajaxService.post('GetGeneratedData', ...) should be called",
            function () {
                // Arrange
                var tableInfoVm = { someVar: "Some value" };
                var responseData = null;
                var getGeneratedDataSuccess = function (data) {
                    responseData = data;
                };
                var errorSpy = jasmine.createSpy();
                // Act
                service.getGeneratedData(tableInfoVm, getGeneratedDataSuccess, errorSpy);
                // Assert
                expect(ajaxServiceMock.post).toHaveBeenCalledWith("GetGeneratedData", tableInfoVm, getGeneratedDataSuccess, errorSpy);
                expect(responseData).toEqual(generatedDataVm);
                expect(errorSpy).not.toHaveBeenCalled();
            });

        it("getGeneratedData(): ajaxService.post('GetGeneratedData', ...) should call error function",
            function () {
                // Arrange
                isErrorWhenPost = true;
                var tableInfoVm = { someVar: "Some value" };
                var successSpy = jasmine.createSpy();
                var errorSpy = jasmine.createSpy();
                // Act
                service.getGeneratedData(tableInfoVm, successSpy, errorSpy);
                // Assert
                expect(ajaxServiceMock.post).toHaveBeenCalledWith("GetGeneratedData", tableInfoVm, jasmine.any(Function), errorSpy);
                expect(successSpy).not.toHaveBeenCalled();
                expect(errorSpy).toHaveBeenCalled();
            });

    });
