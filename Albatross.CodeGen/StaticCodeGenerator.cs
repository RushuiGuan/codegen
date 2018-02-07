using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public class StaticCodeGenerator : ICodeGenerator<object, object> {
		string content;

		public event Func<StringBuilder, ICodeGeneratorFactory, IEnumerable<object>> Yield { add { } remove { } }

		public StringBuilder Build(StringBuilder sb, object source, object option, ICodeGeneratorFactory factory, out IEnumerable<object> used) {
			used = new object[] { this };
			return sb.Append(content);
		}

		public void Configure(object data) {
			this.content = Convert.ToString(data);
		}
	}
}