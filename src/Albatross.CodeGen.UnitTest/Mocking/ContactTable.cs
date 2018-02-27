using Albatross.CodeGen.Database;
using Moq;
using System.Collections.Generic;

namespace Albatross.CodeGen.UnitTest.Mocking {
	/// <summary>
	/// create the following table by mocking the following interfaces: IGetTableColumns, IGetTableIdentityColumn, IGetTablePrimaryKey
	/// 
	/// create table test.Contact(
	///		ContactID int identity(1,1) not null,
	///		constraint UQ_Contact_SyID unique(SyID),
	///		
	///		Domain varchar(100),
	///		Login varchar(100),
	///		constraint PK_Contact primary key clustered (Domain, Login),
	///		
	///		FirstName nvarchar(100) not null,
	///		LastName nvarchar(100) not null,
	///		MiddleName nvarchar(100),
	///		
	///		Gender char not null,
	///		Cell varchar(50),
	///		Address nvarchar(200),
	///		
	/// 
	///		Created datetime not null,
	///		CreatedBy varchar(100) not null,
	///		Modified datetime not null,
	///		ModifiedBy varchar(100) not null,
	/// )
	/// </summary>


	public class ContactTable : TableMocking {
		public ContactTable(Mock<IGetTableColumn> getTableColumns, Mock<IGetTablePrimaryKey> getPrimaryKeys, Mock<IGetTableIdentityColumn> getIdentityColumn) : base(getTableColumns, getPrimaryKeys, getIdentityColumn) {
		}

		public readonly static DatabaseObject Table = new DatabaseObject { Name = "Contact", Schema = "test" };

		public override string TableName => Table.Name;
		public override string Schema => Table.Schema;

		public override IEnumerable<Column> Columns => new Column[] {
			new Column { Name = "ContactID", IdentityColumn = true, DataType="int", IsNullable=false,OrdinalPosition = 0 },
				new Column{ Name="Domain", DataType = "varchar", MaxLength=100, IsNullable=false,OrdinalPosition = 1},
				new Column{ Name="Login", DataType = "varchar", MaxLength=100, IsNullable=false,OrdinalPosition = 2},

				new Column{ Name="FirstName", DataType = "nvarchar", MaxLength=100, IsNullable=false,OrdinalPosition = 3},
				new Column{ Name="LastName", DataType = "nvarchar", MaxLength=100, IsNullable=false,OrdinalPosition = 4},
				new Column{ Name="MiddleName", DataType = "nvarchar", MaxLength=100, IsNullable=true,OrdinalPosition = 5},

				new Column { Name = "Gender", DataType="char", MaxLength=20, IsNullable=false, OrdinalPosition = 6},
				new Column{ Name="Cell", DataType = "varchar", MaxLength=100, IsNullable=true,OrdinalPosition = 7},
				new Column{ Name="Address", DataType = "nvarchar", MaxLength=100, IsNullable=true,OrdinalPosition = 8},

				new Column { Name = "Created", DataType="datetime", IsNullable=false, OrdinalPosition = 9},
				new Column { Name = "CreatedBy", DataType="varchar", MaxLength=100, IsNullable=false, OrdinalPosition = 10},
				new Column { Name = "Modified", DataType="datetime", IsNullable=false, OrdinalPosition = 11},
				new Column { Name = "ModifiedBy", DataType="varchar", MaxLength=100, IsNullable=false, OrdinalPosition = 12},
		};
		public override IEnumerable<Column> PrimaryKeys => new Column[] {
			new Column{ Name="Domain", DataType = "varchar", MaxLength=100, IsNullable=false,},
			new Column{ Name="Login", DataType = "varchar", MaxLength=100, IsNullable=false,},
		};
		public override Column IdentityColumn => new Column { Name = "ContactID", DataType = "int", IsNullable = false, };
	}
}
