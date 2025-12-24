using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Declarations {
	public record class EnumDeclaration : CodeNode, IDeclaration, ICodeElement {
		public IdentifierNameExpression Identifier { get; }
		public EnumDeclaration(string name) {
			this.Identifier = new IdentifierNameExpression(name);
			Items = new ListOfNodes<EnumItemExpression> {
				Separator = ",",
				Multiline = true,
			};
		}
		public IEnumerable<IKeyword> Modifiers { get; init; } = [];
		public ListOfNodes<EnumItemExpression> Items { get; }

		public override IEnumerable<ICodeNode> Children => [Identifier, Items];

		public override TextWriter Generate(TextWriter writer) {
			writer.Append("export ").Append("enum ");
			using (var scope = writer.Code(Identifier).BeginScope()) {
				scope.Writer.Code(Items);
			}
			writer.WriteLine();
			return writer;
		}
	}
}