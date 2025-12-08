using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfArguments : ListOfSyntaxNodes<IExpression>, IExpression {
		public ListOfArguments(params IEnumerable<IExpression> nodes) : base(nodes) {
			Prefix = "(";
			PostFix = ")";
		}
	}
}