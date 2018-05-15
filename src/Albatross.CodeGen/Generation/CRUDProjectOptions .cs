using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Generation {
	/// <summary>
	/// <list type="number">
	///		<description>Database first CRUD Code Generation Steps</description>
	///		<item><description>Create and deploy a sql table</description></item>
	///		<item><description>Generate the POCO class for the table - csharp.table.class</description></item>
	///		<item><description>Generate the Typescript class for the table - typescript.table.class</description></item>
	///		<item><description>Generate and deploy CRUD stored procedures</description></item>
	///		<item><description>Generate C# stored procedure proxy</description></item>
	///		<item><description>Generate C# CRUD interfaces</description></item>
	///		<item><description>Generate the concret class for the C# CRUD interfaces</description></item>
	/// </list>
	/// </summary>
	public class CRUDProjectOptions {
		public string RootPath { get; set; }
		public string TypeScriptPath { get; set; }
		public string InterfacePath { get; set; }
		public string ImplementationPath { get; set; }
		public string ClassPath { get; set; }
		public string StoredProcedurePath { get; set; }
		public string DataLayerApiPath { get; set; }

		public Albatross.Database.DatabasePermission[] ProcedurePermissions { get; set; }
		public Dictionary<string, string> ClassPropertyTypeOverrides { get; set; } = new Dictionary<string, string>();

		public string RootNamespace { get; set; }
		public string InterfaceNamespace { get; set; }
		public string ImplementationNamespace { get; set; }
		public string ClassNamespace { get; set; }
		public string DataLayerApiNamespace { get; set; }

		public string Name { get; set; }
		public string Schema { get; set; }
		public string Server { get; set; }
		public string Database { get; set; }
	}
}
