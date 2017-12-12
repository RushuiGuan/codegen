using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class CompositeTableQuery : ITableBasedQuery {
		string[] _parts;
		public CompositeTableQuery(string scenario, params string[] parts) {
			Scenario = scenario;
			_parts = parts;
		}

		public string Scenario { get; private set; }
		public string Description { get; set; }

		public virtual StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			for(int i=0; i<_parts.Length; i++) { 
				var builder = factory.Get(_parts[i]);
				builder.Build(schema, table, factory, sb, @params);
				if (i == _parts.Length - 1) {
					sb.Semicolon();
				} else {
					sb.AppendLine();
				}
			}
			return sb;
		}
	}
}
