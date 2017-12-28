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

		public IEnumerable<SourceType> Get() {
			List<SourceType> list = new List<SourceType>();
			foreach (var asm in settingRepository.GetAssembly()) {
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
