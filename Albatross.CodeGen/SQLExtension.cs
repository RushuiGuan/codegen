using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public static class SQLExtension {
		public static StringBuilder EscapeName(this StringBuilder sb, string name) {
			return sb.Append('[').Append(name).Append(']');
		}
	}
}
