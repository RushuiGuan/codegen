using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class SourceTypeFactory : ISourceTypeFactory {
		IFactory<IEnumerable<Assembly>> getAssembly;

		public SourceTypeFactory(IFactory<IEnumerable<Assembly>> getAssembly) {
			this.getAssembly = getAssembly;
		}

		public IEnumerable<SourceType> Get() {
			List<SourceType> list = new List<SourceType>();
			foreach (var asm in getAssembly.Get()) {
				foreach (var type in asm.GetTypes()) {
					SourceTypeAttribute attrib = type.GetCustomAttribute<SourceTypeAttribute>();
					if (attrib != null) {
						list.Add(new SourceType {
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
