using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public class StaticCodeGenerator : ICodeGenerator {
		string content;
		public int TabLevel { get; set; }

		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }
		public IEnumerable<object>  Generate(StringBuilder sb, object source, ICodeGeneratorOption option) {
			sb.Tab(TabLevel).Append(content);
			return new[] { this };
		}
		public void Configure(object data) {
			this.content = Convert.ToString(data);
		}
	}
}