using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	public class ColumnNotFoundException : Exception {
		public ColumnNotFoundException(Table table) : this(table.Schema, table.Name) { }
		public ColumnNotFoundException(string schema, string table) : base($"Available column not found for table [{schema}].[{table}]") { }
	}
}
