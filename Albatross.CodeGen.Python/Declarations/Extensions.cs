using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public static class Extensions {
		public static ListOfNodes<ParameterDeclaration> WithSelf(this ListOfNodes<ParameterDeclaration> parameters) {
			var list = new List<ParameterDeclaration> {
				Defined.Parameters.Self,
			};
			list.AddRange(parameters);
			return new ListOfNodes<ParameterDeclaration>(list);
		}
	}
}