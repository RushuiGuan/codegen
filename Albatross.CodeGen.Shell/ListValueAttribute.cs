using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	[AttributeUsage( AttributeTargets.Class)]
	public class ListValueAttribute : Attribute {
		public Type ValueType { get; }
		public ListValueAttribute(Type type) {
			ValueType = type;
		}
	}
}
