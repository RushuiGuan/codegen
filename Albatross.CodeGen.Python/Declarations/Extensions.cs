using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public static class Extensions {
		public static ListOfSyntaxNodes<ParameterDeclaration> WithSelf(this ListOfSyntaxNodes<ParameterDeclaration> parameters) {
			var list  = new List<ParameterDeclaration> {
				Defined.Parameters.Self,
			};
			list.AddRange(parameters.Nodes);
			return new ListOfSyntaxNodes<ParameterDeclaration>(list);
		}
	}
}