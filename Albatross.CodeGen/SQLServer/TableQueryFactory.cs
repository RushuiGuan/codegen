using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableQueryFactory : ITableQueryFactory {
		Dictionary<string, ITableBasedQuery> _registrations = new Dictionary<string, ITableBasedQuery>(StringComparer.InvariantCultureIgnoreCase);

		public TableQueryFactory(IEnumerable<ITableBasedQuery> registrations) {
			foreach (var item in registrations) {
				_registrations.Add(item.Scenario, item);
			}
		}

		public IEnumerable<ITableBasedQuery> Registrations => this._registrations.Values;

		public ITableBasedQuery Get(string scenario) {
			ITableBasedQuery result;
			if (!_registrations.TryGetValue(scenario, out result)) {
				throw new ScenarioNotRegisteredException(scenario);
			}
			return result;
		}
	}
}
