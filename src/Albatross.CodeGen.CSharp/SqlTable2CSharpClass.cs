using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Core;
using Albatross.Database;

namespace Albatross.CodeGen.SqlServer {
	/// <summary>
	/// Create a CSharp class from a SQL Stored procedure
	/// </summary>
	[CodeGenerator("table-to-class", GeneratorTarget.CSharp, Description ="Generate a C Sharp class from a sql server table")]
	public class SqlTable2CSharpClass : CodeGeneratorBase<Table, Class> {
		IDatabaseTableToClass generateClassFromDatabaseTable;
		IOverrideClassObject overrideClassObject;
		IWriteObject<Class> writeClass;

		public SqlTable2CSharpClass(GenerateClassFromDatabaseTable generateClassFromDatabaseTable, IOverrideClassObject overrideClassObject, IWriteObject<Class> writeClass) {
			this.generateClassFromDatabaseTable = generateClassFromDatabaseTable;
			this.overrideClassObject = overrideClassObject;
			this.writeClass = writeClass;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table source, Class classOption) {
			Class @class = this.generateClassFromDatabaseTable.Get(source);
			@class = this.overrideClassObject.Get(@class, classOption);
			sb.Write(writeClass, @class);
			return new object[] { this };
		}
	}
}