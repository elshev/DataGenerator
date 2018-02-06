export interface ColumnInfo {
    name: string;
    columnType: number;
    systemColumnType: number;
    isComputed: boolean;
    isIdentity: boolean;
    isNullable: boolean;
    defaultFormat: string;
    format: string;
    identitySeed: number;
    startValue: number; //???
    identityIncrement: number;
    incrementValue: number; //???
    maxLength: number;
    precision: number;
    maxPrecision: number;
    nullPercent: number;
    scale: number;
}