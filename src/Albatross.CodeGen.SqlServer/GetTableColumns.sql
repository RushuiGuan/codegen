select
	COLUMN_NAME as Name,
	ORDINAL_POSITION as OrdinalPosition,
	case when IS_NULLABLE = 'yes' then cast(1 as bit) else cast(0 as bit) end as IsNullable,
	DATA_TYPE as DataType,
	CHARACTER_MAXIMUM_LENGTH as MaxLength,
	case when DATA_TYPE = 'datetime' or DATA_TYPE = 'datetime2' then DATETIME_PRECISION else numeric_precision end as Precision,
	NUMERIC_SCALE as Scale,
	cast(COLUMNPROPERTY(object_id(TABLE_SCHEMA+'.'+TABLE_NAME), COLUMN_NAME, 'IsIdentity') as bit) as IdentityColumn,
	cast(COLUMNPROPERTY(object_id(TABLE_SCHEMA+'.'+TABLE_NAME), COLUMN_NAME, 'IsComputed') as bit) as ComputedColumn
from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @table and TABLE_SCHEMA = @schema
order by OrdinalPosition asc