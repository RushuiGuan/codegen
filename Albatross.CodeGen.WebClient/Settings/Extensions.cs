using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.Settings {
	public static class Extensions {
		public static SymbolFilter[] ControllerMethodFilters(this CodeGenSettings settings)
			=> settings.ControllerMethodFilters.Where(x => x.HasValue).Select(x => new SymbolFilter(x)).ToArray();

		public static SymbolFilter[] DtoFilters(this CodeGenSettings settings)
			=> settings.DtoFilters.Where(x => x.HasValue).Select(x => new SymbolFilter(x)).ToArray();

		public static SymbolFilter[] EnumFilters(this CodeGenSettings settings)
			=> settings.EnumFilters.Where(x => x.HasValue).Select(x => new SymbolFilter(x)).ToArray();

		public static SymbolFilter[] ControllerFilters(this CodeGenSettings settings)
			=> settings.ControllerFilters.Where(x => x.HasValue).Select(x => new SymbolFilter(x)).ToArray();
		
		/// <summary>
		/// Implement the same behavior with the single pattern filter.  Include wins over exclude
		/// </summary>
		/// <param name="filters"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool ShouldKeep(this IEnumerable<SymbolFilter> filters, string name) {
			bool excluded = false;
			foreach (var filter in filters) {
				if (filter.Include(name)) {
					return true;
				} else {
					excluded = excluded || filter.Exclude(name);
				}
			}
			return !excluded;
		}
	}
}