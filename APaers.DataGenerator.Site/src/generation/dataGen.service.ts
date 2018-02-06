import {Injectable} from "@angular/core";
import {Observable} from "rxjs/Observable";

import {HttpService} from "../common/http.service";
import {TableInfo} from "./tableInfo.model";
import {GeneratedData} from "./generatedData.model";

@Injectable()
export class DataGenService {
    constructor(private httpService: HttpService) {}

    getTableInfo(script: string, sqlType: number): Observable<TableInfo> {
        const scriptVm = { sqlType: sqlType, createTableScript: script };
        // ToDo: intercept and modify dates if needed. See processTableInfo function from the old code.
        return this.httpService.post<TableInfo>("GetTableInfo", scriptVm);
    }

    getGeneratedData(tableInfo: TableInfo) {
        return this.httpService.post<GeneratedData>("GetGeneratedData", tableInfo);
    }
}


/*        function getTableInfo(script, sqlType, successFunction, errorFunction) {
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
*/
