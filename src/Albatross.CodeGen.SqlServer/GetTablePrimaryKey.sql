select
	columns.COLUMN_NAME as Name,
	columns.ORDINAL_POSITION as OrdinalPosition,
	case when columns.IS_NULLABLE = 'yes' then cast(1 as bit) else cast(0 as bit) end as IsNullable,
	columns.DATA_TYPE as DataType,
	columns.CHARACTER_MAXIMUM_LENGTH as MaxLength,
	case when columns.DATA_TYPE = 'datetime' or columns.DATA_TYPE = 'datetime2' then columns.DATETIME_PRECISION else columns.numeric_precision end as Precision,
	columns.NUMERIC_SCALE as Scale,
	cast(COLUMNPROPERTY(object_id(columns.TABLE_SCHEMA+'.'+columns.TABLE_NAME), columns.COLUMN_NAME, 'IsIdentity') as bit) as IdentityColumn,
	cast(COLUMNPROPERTY(object_id(columns.TABLE_SCHEMA+'.'+columns.TABLE_NAME), columns.COLUMN_NAME, 'IsComputed') as bit) as ComputedColumn
from INFORMATION_SCHEMA.COLUMNS
join INFORMATION_SCHEMA.Constraint_Column_Usage column_constraint on columns.TABLE_SCHEMA = column_constraint.TABLE_SCHEMA and columns.TABLE_NAME = column_constraint.TABLE_NAME and columns.COLUMN_NAME = column_constraint.COLUMN_NAME
join INFORMATION_SCHEMA.TABLE_CONSTRAINTS table_constraint on columns.TABLE_SCHEMA = table_constraint.TABLE_SCHEMA and columns.TABLE_NAME = table_constraint.TABLE_NAME and column_constraint.CONSTRAINT_NAME = table_constraint.CONSTRAINT_NAME
where columns.TABLE_NAME = @table and columns.TABLE_SCHEMA = @schema and table_constraint.constraint_type = 'primary key'


