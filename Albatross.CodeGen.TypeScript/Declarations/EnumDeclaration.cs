using Albatross.CodeGen.TypeScript.Expressions;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Declarations {
	public record class EnumDeclaration : CodeNode, IDeclaration, ICodeElement {
		public IdentifierNameExpression Identifier { get; }
		public EnumDeclaration(string name) {
			this.Identifier = new IdentifierNameExpression(name);
			Items = new ListOfNodes<EnumItemExpression> {
				Prefix = " {",
				PostFix = "}",
				Separator = ",",
				Multiline = true,
			};
		}
		public IEnumerable<IKeyword> Modifiers { get; init; } = [];
		public ListOfNodes<EnumItemExpression> Items { get; }

		public override IEnumerable<ICodeNode> Children => [Identifier, Items];

		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Defined.Keywords.Export).Code(Defined.Keywords.Enum).Code(Identifier).Code(Items);
		}
	}
}