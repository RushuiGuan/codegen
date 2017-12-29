using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Tool.ViewModel {
	public class CodeGenerator {
		public CodeGenerator(ICodeGenerator codeGen) {
			Handle = codeGen;
		}

		public string SourceType { get { return Handle.SourceType.Name; } }
		public string Name { get { return Handle.Name; } }
		public string Category { get { return Handle.Category; } }
		public string Description { get { return Handle.Description; } }
		public string Target { get { return Handle.Target; } }
		public string Type { get { return Handle.GetType().FullName; } }
		public string Assembly { get { return Handle.GetType().Assembly.FullName; } }
		public string Location { get { return Handle.GetType().Assembly.Location; } }

		public ICodeGenerator Handle { get; private set; }
	}
}
