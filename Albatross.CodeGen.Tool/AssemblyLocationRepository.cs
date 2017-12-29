using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.Logging.Core;
using System.Reflection;

namespace Albatross.CodeGen.Tool {
	public class AssemblyLocationRepository : JsonFileRepository<AssemblyLocationSetting> {
		public AssemblyLocationRepository(ILogFactory logFactory) : base(logFactory) {
		}

		public override string FileName => "AssemblyLocations";


		public IEnumerable<Assembly> GetAssembly() {
			List<Assembly> list = new List<Assembly>();
			var setting = Get();
			if (setting?.Locations != null) {
				foreach (var path in setting.Locations) {
					if (File.Exists(path)) {
						if (TryLoadFile(path, out Assembly asm)) {
							list.Add(asm);
						}
					} else if (Directory.Exists(path)) {
						foreach (var file in Directory.GetFiles(path, "*.dll")) {
							if (TryLoadFile(file, out Assembly asm)) {
								list.Add(asm);
							}
						}
					} else {
						//try to do a pattern match
						try {
							string folder = Path.GetDirectoryName(path);
							if (Directory.Exists(folder)) {
								string pattern = Path.GetFileName(path);
								foreach (var file in Directory.GetFiles(folder, pattern)) {
									if (TryLoadFile(file, out Assembly asm)) {
										list.Add(asm);
									}
								}
							}
						} catch (Exception err) {
							_log.Error(err);
						}
					}
				}
			}
			return list;
		}


		bool TryLoadFile(string name, out Assembly asm) {
			try {
				asm = Assembly.ReflectionOnlyLoadFrom(name);
				return true;
			} catch (Exception err) {
				_log.Error(err);
				asm = null;
				return false;
			}
		}
	}
}
