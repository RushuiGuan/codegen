using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	public class PrimaryKeyNotFoundException : Exception {
		public PrimaryKeyNotFoundException(Table t) : this(t.Schema, t.Name) { }
		public PrimaryKeyNotFoundException(string schema, string table) : base($"Primary key not found for table [{schema}].[{table}]") { }
	}
}
