using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public class StaticCodeGenerator : ICodeGenerator<object, object> {
		string content;

		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }
		public IEnumerable<object>  Build(StringBuilder sb, object source, object option) {
			sb.Append(content);
			return new[] { this };
		}
		public void Configure(object data) {
			this.content = Convert.ToString(data);
		}
	}
}