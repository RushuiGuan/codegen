using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {

	[CodeGenerator("string", GeneratorTarget.Any, Category = GeneratorCategory.System, Description = "Print out the source as string")]
	public class StringCodeGenerator : ICodeGenerator {
		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }
		public IEnumerable<object>  Build(StringBuilder sb, object source, object option) {
			if (source != null) {
				sb.Append(source.ToString());
			}
			return new[] { this };
		}
		public void Configure(object data) {
		}

		public void ValidateOption(object option) {
		}
		public void ValidateSource(object source) {
		}
	}
}