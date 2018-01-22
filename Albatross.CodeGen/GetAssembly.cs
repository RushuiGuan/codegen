using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Albatross.Logging.Core;

namespace Albatross.CodeGen {
	public class GetAssembly : IFactory<IEnumerable<Assembly>> {
		IFactory<CodeGenSetting> settingFactory;
		IGetFiles getFiles;
		ILog log;

		public GetAssembly(IFactory<CodeGenSetting> settingFactory, IGetFiles getFiles, ILogFactory logFactory) {
			this.settingFactory = settingFactory;
			this.getFiles = getFiles;
			this.log = logFactory.Get(this);
		}

		///return the assemblies specified in the AssemblyLocation setting as well as the location of the code gen dll
		public IEnumerable<Assembly> Get() {
			var setting = settingFactory.Get();
			List<string> locations = new List<string>();

			locations.Add(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			if (setting.AssemblyLocations != null) {
				locations.AddRange(setting.AssemblyLocations);
			}
			HashSet<string> files = new HashSet<string>();
			foreach (var location in locations) {
				foreach (var file in getFiles.Get(location, ".dll")) {
					files.Add(file.FullName);
				}
			}

			List<Assembly> list = new List<Assembly>();
			foreach (var file in files) {
				try {
					list.Add(Assembly.LoadFile(file));
				} catch (Exception err) {
					log.Error(err);
				}
			}
			return list;
		}
	}
}
