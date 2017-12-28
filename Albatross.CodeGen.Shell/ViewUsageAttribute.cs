using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	[AttributeUsage(AttributeTargets.Class)]
	public class ViewUsageAttribute : Attribute {
		public Type ViewType { get; }

		public ViewUsageAttribute(Type viewType) {
			ViewType = viewType;
		}
	}
}
