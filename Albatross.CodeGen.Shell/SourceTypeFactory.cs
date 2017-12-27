using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public class SourceTypeFactory : ISourceTypeFactory {
		AssemblyLocationRepository settingRepository;
		public SourceTypeFactory(AssemblyLocationRepository repo) {
			settingRepository = repo;
		}

		public IEnumerable<Type> Get() {
			List<Type> list = new List<Type>();
			foreach (var asm in settingRepository.GetAssembly()) {
				foreach (var type in asm.GetTypes()) {
					if (type.GetCustomAttribute<SourceTypeAttribute>() != null) {
						list.Add(type);
					}
				}
			}
			return list;
		}
	}
}
