using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen {
	public interface IGetFiles {
		IEnumerable<FileInfo> Get(string location, string extension);
	}
}
