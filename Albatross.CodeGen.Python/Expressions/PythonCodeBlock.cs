using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.Python.Expressions {
	public record class PythonCodeBlock : CodeBlock {
		public PythonCodeBlock() {
			Prefix = ":";
			Separator = string.Empty;
			Multiline = true;
		}
		public override TextWriter Generate(TextWriter writer) {
			if(nodes.Count == 0) {
				nodes.Add(new PassExpression());
			}
			return base.Generate(writer);
		}
	}
}
