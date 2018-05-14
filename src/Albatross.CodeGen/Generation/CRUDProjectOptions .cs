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


		/// <summary>
		/// Database and server
		/// </summary>
		public Albatross.Database.Database Database { get; set; } = new Albatross.Database.Database();
		public CSharpClassOption ClassOption { get; set; } = new CSharpClassOption();
		public Albatross.Database.DatabasePermission[] DatabasePermissions { get; set; }

		/// <summary>
		/// Table name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Table schema
		/// </summary>
		public string Schema { get; set; }
	}
}
