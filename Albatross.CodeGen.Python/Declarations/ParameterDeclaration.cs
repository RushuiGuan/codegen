using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class ParameterDeclaration : SyntaxNode, IDeclaration {
		public required ITypeExpression Type { get; init; }
		public required IIdentifierNameExpression Identifier { get; init; }
		public override IEnumerable<ISyntaxNode> Children => new List<ISyntaxNode> { Type, Identifier };

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier);
			if (Type != Defined.Types.None) {
				writer.Append(": ").Code(Type);
			}
			return writer;
		}
	}
}