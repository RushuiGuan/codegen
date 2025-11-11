using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.WebClient.Settings {
	public class SymbolFilter {
		readonly Regex? inclusiveFilter;
		readonly Regex? exclusiveFilter;

		public SymbolFilter(SymbolFilterPatterns patterns) {
			if (patterns.Include != null) {
				this.inclusiveFilter = new Regex(patterns.Include, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
			if (patterns.Exclude != null) {
				this.exclusiveFilter = new Regex(patterns.Exclude, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
		}

		public bool Exclude(string name) {
			return this.exclusiveFilter != null && this.exclusiveFilter.IsMatch(name);
		}
		public bool Include(string name) {
			return this.inclusiveFilter != null && this.inclusiveFilter.IsMatch(name);
		}

		/// <summary>
		/// If there is no match by include or exclude filter, method will return true.  If both include and exclude filter are matched, then include wins
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool ShouldKeep(string name) {
			return !Exclude(name) || Include(name) || this.exclusiveFilter == null && this.inclusiveFilter == null;
		}
	}
}