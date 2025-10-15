using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Declarations {
	public record class EnumDeclaration : SyntaxNode, IDeclaration, ICodeElement {
		public IdentifierNameExpression Identifier { get; }
		public EnumDeclaration(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}
		public required ListOfEnumItems Items { get; init; }

		public override IEnumerable<ISyntaxNode> Children => new List<ISyntaxNode> { Identifier, Items };

		public override TextWriter Generate(TextWriter writer) {
			writer.Append("class ").Code(this.Identifier);
			using (var scope = writer.Code(Identifier).BeginScope()) {
				scope.Writer.Code(Items);
			}
			writer.WriteLine();
			return writer;
		}
	}
}