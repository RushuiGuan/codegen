using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Albatross.CodeGen {
	public class AssemblyLocationRepository : JsonFileRepository<AssemblyLocationSetting> {
		public string FileName => "AssemblyLocations.json";
		public string Path { get; }

		public AssemblyLocationRepository(IGetDefaultRepoFolder getDefaultFolder) {
			Path = getDefaultFolder.Get() + "\\" + FileName;
			if (!File.Exists(Path)) {
				var file = File.Create(Path);
				file.Close();
			}
		}

		public IEnumerable<Assembly> GetAssembly() {
			HashSet<string> list = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) {
				System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			};

			var setting = Get(Path);
			list.AddRange(setting?.Locations);

			List<Assembly> result = new List<Assembly>();

			foreach (var path in list) {
				if (File.Exists(path)) {
					result.Add(Assembly.LoadFile(path));
				} else if (Directory.Exists(path)) {
					foreach (var file in Directory.GetFiles(path, "*.dll")) {
						result.Add(Assembly.LoadFile(file));
					}
				} else {
					//try to do a pattern match
					string folder = System.IO.Path.GetDirectoryName(path);
					if (Directory.Exists(folder)) {
						string pattern = System.IO.Path.GetFileName(path);
						foreach (var file in Directory.GetFiles(folder, pattern)) {
							result.Add(Assembly.LoadFile(file));
						}
					}
				}
			}
			return result;
		}
	}
}
