using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class SqlVariableMgmt : IStoreSqlVariable, IGetSqlVariable {
		Dictionary<object, Dictionary<string, Variable>> created = new Dictionary<object, Dictionary<string, Variable>>();

		public void Store(object creator, Variable variable) {
			if (!created.TryGetValue(creator, out Dictionary<string, Variable> values)) {
				values = new Dictionary<string, Variable>(StringComparer.InvariantCultureIgnoreCase);
				created.Add(creator, values);
			}
			values[variable.Name] = variable;
		}

		public IEnumerable<Variable> Get(object creator) {
			if (created.TryGetValue(creator, out Dictionary<string, Variable> values)) {
				return values.Values;
			} else {
				return new Variable[0];
			}
		}
	}
}
