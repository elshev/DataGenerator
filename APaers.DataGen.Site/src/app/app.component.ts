import { Component, OnInit } from "@angular/core";

import {HttpService} from "../common/http.service";
import {DataGenService} from "../generation/dataGen.service";
import {TableInfo} from "../generation/tableInfo.model";
import {GeneratedData} from "../generation/generatedData.model";
import {SqlType, ColumnTypeInfo, ColumnTypeInfos} from "../common/enums";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  providers: [HttpService, DataGenService]
})
export class AppComponent implements OnInit {
    isTableInfoInitialized = false;
    metaScript: string = "create table dbo.Test02 (" +
      "Id int identity(1,1) not null, " +
      "Country varchar(128) not null, " +
      "RegionName nvarchar(128) not null, " +
      "Email varchar(64) not null, " +
      "WholeValue int not null, " +
      "DateTimeValue datetime not null " +
      ");";
    isDataGenerated = false;
    tableInfo: TableInfo = new TableInfo();
    generatedData: GeneratedData;
    metaScriptMessage = null;
    metaDataMessage = null;
    entityCount = 3;
    generatedText = "";
    columnTypeInfos = ColumnTypeInfos;
    // fullNameFormatInfos = Enums.fullNameFormatInfo;

    constructor(private dataGenService: DataGenService) {
    }

    ngOnInit(): void {
      this.getTableInfo();
    }

  private getTableInfo() {
    this.dataGenService
      .getTableInfo(this.metaScript, SqlType.SqlServer)
      .subscribe(this.onGetTableInfo.bind(this), err => this.metaScriptMessage = err);
  }

  private onGetTableInfo(data: TableInfo) {
        this.tableInfo = data;
        this.isTableInfoInitialized = true;
        this.getGeneratedData();
    }

    private getGeneratedData() {
        if (this.tableInfo.entityCount === 0) {
            this.tableInfo.entityCount = this.entityCount;
        }
        this.dataGenService
            .getGeneratedData(this.tableInfo)
            .subscribe(this.onGetGeneratedData.bind(this), err => this.metaDataMessage = err);
    }

    private onGetGeneratedData(data: GeneratedData) {
        this.generatedData = data;
        this.isDataGenerated = true;
    }
}
