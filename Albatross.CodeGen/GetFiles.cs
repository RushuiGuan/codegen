using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen {
	public class GetFiles : IGetFiles {
		public IEnumerable<FileInfo> Get(string location, string extension) {
			List<FileInfo> list = new List<FileInfo>();

			if (File.Exists(location)) {
				list.Add(new FileInfo(location));
			} else if (Directory.Exists(location)) {
				string pattern = string.IsNullOrEmpty(extension) ? "*.*" : "*" + extension;
				foreach (var file in Directory.GetFiles(location, pattern)) {
					list.Add(new FileInfo(file));
				}
			} else {
				//try to do a pattern match
				string folder = System.IO.Path.GetDirectoryName(location);
				if (Directory.Exists(folder)) {
					string pattern = System.IO.Path.GetFileName(location);
					foreach (var file in Directory.GetFiles(folder, pattern)) {
						list.Add(new FileInfo(file));
					}
				}
			}
			return list;
		}
	}
}
