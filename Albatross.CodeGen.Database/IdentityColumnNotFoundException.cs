using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database{

	public class IdentityColumnNotFoundException : Exception {
		public IdentityColumnNotFoundException(Table table) : this(table.Schema, table.Name) { }
		public IdentityColumnNotFoundException(string schema, string table) : base($"Identity column not found for table [{schema}].[{table}]") { }
	}
}
