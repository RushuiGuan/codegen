using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class OptionTypeFactory : IFactory<IEnumerable<OptionType>> {
		IFactory<IEnumerable<Assembly>> getAssembly;

		public OptionTypeFactory(IFactory<IEnumerable<Assembly>> getAssembly) {
			this.getAssembly = getAssembly;
		}

		public IEnumerable<OptionType> Get() {
			List<OptionType> list = new List<OptionType>();
			foreach (var asm in getAssembly.Get()) {
				foreach (var type in asm.GetTypes()) {
					OptionTypeAttribute attrib = type.GetCustomAttribute<OptionTypeAttribute>();
					if (attrib != null) {
						list.Add(new OptionType {
							ObjectType = type,
							Description = attrib.Description,
						});
					}
				}
			}
			return list;
		}
	}
}
