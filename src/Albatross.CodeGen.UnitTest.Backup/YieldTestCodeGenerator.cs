using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	public class YieldTestCodeGenerator : CodeGeneratorBase<object, object> {
		public override IEnumerable<object> Build(StringBuilder sb, object source, object option) {
			sb.AppendLine("begin");
			IEnumerable<object> objects;
			List<object> list = new List<object> { this };

			while (true) {
				sb.Tab();
				objects = InvokeYield(sb);
				if (objects?.Count() > 0) {
					list.AddRange(objects);
				} else {
					sb.Length--;
					break;
				}
			}
			sb.AppendLine();
			sb.Append("end");
			return list;
		}
	}
}