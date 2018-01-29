using System;
using System.Collections.Generic;
using APaers.DataGen.Abstract.Generate;

namespace APaers.DataGen.SqlServer
{
    public class SqlListener : TSqlParserBaseListener
    {
        public TableInfo TableInfo { get; private set; }

        public override void EnterCreate_table(TSqlParser.Create_tableContext context)
        {
            base.EnterCreate_table(context);
            TableInfo = new TableInfo {Columns = new List<ColumnInfo>()};
            ProcessTableName(context.table_name());
            ProcessColumnDefTableConstraints(context.column_def_table_constraints());
        }

        private void ProcessTableName(TSqlParser.Table_nameContext context)
        {
            if (TableInfo == null) return;
            TableInfo.Name = context.GetText();
        }

        public void ProcessColumnDefTableConstraints(TSqlParser.Column_def_table_constraintsContext context)
        {
            foreach (var constraintContext in context.column_def_table_constraint())
            {
                ProcessColumnDefTableConstraint(constraintContext);
            }
        }

        public void ProcessColumnDefTableConstraint(TSqlParser.Column_def_table_constraintContext context)
        {
            ProcessColumnDefinition(context.column_definition());
        }

        public void ProcessColumnDefinition(TSqlParser.Column_definitionContext context)
        {
            if (TableInfo == null) return;
            string columnName = context.id(0).GetText();
            string columnTypeName = "int";
            var dataTypeContext = context.data_type();
            if (dataTypeContext != null)
            {
                columnTypeName = dataTypeContext.id().GetText();
            }
            SystemColumnType systemColumnType = SqlServerHelper.SqlServerToSystemColumnType(columnTypeName);
            ColumnInfo columnInfo = ColumnInfoHelper.CreateColumnInfo(systemColumnType, columnName);
            columnInfo.IsComputed = context.AS()?.GetText().ToLower() == "as";
            if (dataTypeContext != null)
            {
                string decimal1Text = dataTypeContext.DECIMAL(0)?.GetText();
                string decimal2Text = dataTypeContext.DECIMAL(1)?.GetText();
                int decimal1 = decimal1Text == null ? 0 : int.Parse(decimal1Text);
                int decimal2 = decimal2Text == null ? 0 : int.Parse(decimal2Text);
                columnInfo.IsIdentity = dataTypeContext.IDENTITY()?.GetText().ToLower() == "identity";
                if (columnInfo.IsIdentity)
                {
                    columnInfo.IdentitySeed = decimal1;
                    columnInfo.IdentityIncrement = decimal2;
                }
                else
                {

                    columnInfo.MaxLength = decimal1;
                    columnInfo.SetMaxPrecision(columnInfo.MaxLength);
                    columnInfo.Scale = decimal2;
                }
            }

            bool isNot = !string.IsNullOrWhiteSpace(context.null_notnull().NOT()?.GetText());
            bool isNull = !string.IsNullOrWhiteSpace(context.null_notnull().NULL()?.GetText());
            columnInfo.IsNullable = !(isNot && isNull);

            TableInfo.Columns.Add(columnInfo);
        }
    }
}