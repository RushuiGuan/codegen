using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class CreateVariable : ICreateVariable, IGetVariable {
		Dictionary<ICodeGenerator, Dictionary<string, string>> created = new Dictionary<ICodeGenerator, Dictionary<string, string>>();

		public void Create(ICodeGenerator creator, string name, string type) {
			if (!created.TryGetValue(creator, out Dictionary<string, string> values)) {
				values = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
				created.Add(creator, values);
			}
			values[name] = type;
		}

		public IDictionary<string, string> Get(ICodeGenerator generator) {
			if (created.TryGetValue(generator, out Dictionary<string, string> values)) {
				return values;
			} else {
				return new Dictionary<string, string>();
			}
		}
	}
}
