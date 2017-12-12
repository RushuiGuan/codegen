using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class BuiltInColumnFactory : IBuiltInColumnFactory {
		Dictionary<string, BuiltInColumn> _registrations = new Dictionary<string, BuiltInColumn>(StringComparer.InvariantCultureIgnoreCase);

		public BuiltInColumnFactory(IEnumerable<BuiltInColumn> columns) {
			foreach (var item in columns) {
				_registrations.Add(item.Name, item);
			}
		}

		public BuiltInColumn Get(string name) {
			BuiltInColumn c;
			if (_registrations.TryGetValue(name, out c)) {
				return c;
			} else {
				throw new BuiltInColumnNotRegisteredException(name);
			}
		}
	}
}
