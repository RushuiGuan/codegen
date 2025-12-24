using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfArguments : ListOfNodes<IExpression>, IExpression {
		public ListOfArguments() {
			Prefix = "(";
			PostFix = ")";
		}
	}
}