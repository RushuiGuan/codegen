using Albatross.CodeGen.Database;
using System.Collections.Generic;

namespace Albatross.CodeGen.UnitTest.Mocking {
	/// <summary>
	/// create the following table by mocking the following interfaces: IGetTableColumns, IGetTableIdentityColumn, IGetTablePrimaryKey
	/// 
	/// create table Symbol(
	///		SyID int identity(1,1) not null,
	///		constraint UQ_Symbol_SyID unique(SyID),
	///		
	///		SyCode varchar(100) not null,
	///		constraint PK_Symbol_SyCode primary key clustered (SyCode),
	///		
	///		CuID int not null,
	///		Outshares bigint not null,
	///		CoID int not null,
	///		SnID int,
	/// )
	/// </summary>


	public class SymbolTable : TableMocking {
		public override string TableName => "Symbol";
		public override string Schema => "test";
		public override Table Table => new Table { Name = TableName, Schema = Schema };
		public override IEnumerable<Column> Columns => new Column[] {
			new Column { Name = "SyID", IdentityColumn = true, DataType="int", IsNullable=false, OrdinalPosition = 0 },
				new Column{ Name="SyCode", DataType = "varchar", MaxLength=100, IsNullable=false, OrdinalPosition = 1},

				new Column { Name = "CuID", DataType="int", IsNullable=false,  OrdinalPosition =2 },
				new Column { Name = "OutShares", DataType="bigint", IsNullable=false, OrdinalPosition = 3 },
				new Column { Name = "CoID", DataType="int", IsNullable=false, OrdinalPosition = 4 },
				new Column { Name = "SnID", DataType="int", IsNullable=true,  OrdinalPosition = 5},
		};
		public override IEnumerable<Column> PrimaryKeys => new Column[] {
			new Column{ Name="SyCode", DataType = "varchar", MaxLength=100, IsNullable=false,},
		};
		public override Column IdentityColumn => new Column { Name = "SyID", DataType = "int", IsNullable = false, };
	}
}
