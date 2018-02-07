using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	public class YieldTestCodeGenerator : ICodeGenerator<object, object> {
		public event Func<StringBuilder, ICodeGeneratorFactory, IEnumerable<object>> Yield;

		public StringBuilder Build(StringBuilder sb, object source, object option, ICodeGeneratorFactory factory, out IEnumerable<object> used) {
			sb.AppendLine("begin");
			IEnumerable<object> objects;
			List<object> list = new List<object>();
			list.Add(this);

			while(true){
				sb.Tab();
				objects = Yield?.Invoke(sb, factory);
				if (objects?.Count() > 0) {
					list.AddRange(objects);
				} else {
					sb.Length--;
					break;
				}
			} 
			sb.AppendLine();
			sb.Append("end");
			used = list;
			return sb;
		}

		public void Configure(object data) {
		}
	}
}
