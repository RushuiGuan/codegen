using static Albatross.CodeGen.UnitTest.Extension;
using Albatross.CodeGen.Database;
using Albatross.Database;
using Moq;
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
	///		OutShares bigint not null,
	///		CoID int not null,
	///		SnID int,
	/// )
	/// </summary>


	public class SymbolTable : TableMocking {
		public static readonly Table Table = new Table{
			Name = "Symbol",
			Schema = "test",
		};

		public SymbolTable(Mock<IGetTable> getTable) : base(getTable) { }

		public override string TableName => Table.Name;
		public override string Schema => Table.Schema;

		public override IEnumerable<Column> Columns => new Column[] {
			new Column { Name = "SyID", IsIdentity = true, Type = Int(), IsNullable=false, OrdinalPosition = 0 },
				new Column{ Name="SyCode", Type = NonUnicodeString(100),IsNullable=false, OrdinalPosition = 1},

				new Column { Name = "CuID", Type = Int(), IsNullable=false,  OrdinalPosition =2 },
				new Column { Name = "OutShares", Type = BigInt(), IsNullable=false, OrdinalPosition = 3 },
				new Column { Name = "CoID", Type = Int(), IsNullable=false, OrdinalPosition = 4 },
				new Column { Name = "SnID", Type = Int(), IsNullable=true,  OrdinalPosition = 5},
		};
		public override IEnumerable<IndexColumn> PrimaryKeys => new IndexColumn[] {
			new IndexColumn{ Name = "SyCode",  },
		};

		public override Column IdentityColumn => new Column { Name = "SyID", Type = Int(), IsNullable = false, };
	}
}
