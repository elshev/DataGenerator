import {ColumnInfo} from "./columnInfo.model";
import {SqlType} from "../common/enums";

export class TableInfo {
    sqlType: number = SqlType.SqlServer;
    name: string = "";
    columns: ColumnInfo[] = [];
    entityCount: number = 3;

}
