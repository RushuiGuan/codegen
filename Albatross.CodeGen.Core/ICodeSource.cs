using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Core {
	public interface ICodeSource {
		string SourceType { get; }
		string Name { get; }
	}
}
