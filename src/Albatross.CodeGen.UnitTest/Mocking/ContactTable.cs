using static Albatross.CodeGen.UnitTest.Extension;
using Albatross.CodeGen.Database;
using Albatross.Database;
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
	///		Gender char(20) not null,
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
		public ContactTable(Mock<IGetTable> getTable) : base(getTable) {
		}

		public readonly static Table Table = new Table{ Name = "Contact", Schema = "test" };

		public override string TableName => Table.Name;
		public override string Schema => Table.Schema;

		public override IEnumerable<Column> Columns => new Column[] {
			new Column { Name = "ContactID", IsIdentity = true, Type = Int(), IsNullable=false,OrdinalPosition = 0 },
				new Column{ Name="Domain", Type = NonUnicodeString(100), IsNullable=false,OrdinalPosition = 1},
				new Column{ Name="Login", Type = NonUnicodeString(100), IsNullable=false,OrdinalPosition = 2},

				new Column{ Name="FirstName", Type = UnicodeString(100), IsNullable=false,OrdinalPosition = 3},
				new Column{ Name="LastName", Type = UnicodeString(100), IsNullable=false,OrdinalPosition = 4},
				new Column{ Name="MiddleName", Type = UnicodeString(100), IsNullable=true,OrdinalPosition = 5},

				new Column { Name = "Gender", Type = NonUnicodeFixedString(20), IsNullable=false, OrdinalPosition = 6},
				new Column{ Name="Cell", Type = NonUnicodeString(50), IsNullable=true,OrdinalPosition = 7},
				new Column{ Name="Address", Type = UnicodeString(200), IsNullable=true,OrdinalPosition = 8},

				new Column { Name = "Created", Type =DateTime(), IsNullable=false, OrdinalPosition = 9},
				new Column { Name = "CreatedBy", Type = NonUnicodeString(100), IsNullable=false, OrdinalPosition = 10},
				new Column { Name = "Modified", Type=DateTime(), IsNullable=false, OrdinalPosition = 11},
				new Column { Name = "ModifiedBy", Type = NonUnicodeString(100), IsNullable=false, OrdinalPosition = 12},
		};
		public override IEnumerable<IndexColumn> PrimaryKeys => new IndexColumn[] {
			new IndexColumn{ Name="Domain", },
			new IndexColumn{ Name="Login", },
		};
		public override Column IdentityColumn => new Column { Name = "ContactID", Type = Int(), IsNullable = false, };
	}
}
