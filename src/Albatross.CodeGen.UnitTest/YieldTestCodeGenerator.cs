using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	public class YieldTestCodeGenerator : ICodeGenerator<object, object> {
		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Generate(StringBuilder sb, IDictionary<string, string> customCode, object source, object option) {
			sb.AppendLine("begin");
			IEnumerable<object> objects;
			List<object> list = new List<object> { this };
			StringBuilder scoped = new StringBuilder();
			objects = Yield?.Invoke(scoped);
			if (objects != null) { list.AddRange(objects); }
			sb.Tabify(scoped.ToString(), 1);
			sb.AppendLine();
			sb.Append("end");
			return list;
		}

		public void Configure(object data) {
		}
	}
}