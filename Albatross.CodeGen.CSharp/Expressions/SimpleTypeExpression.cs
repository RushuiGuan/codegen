using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class SimpleTypeExpression : SyntaxNode, ITypeExpression {
		public SimpleTypeExpression(params string[] name) {
			if(name.Length == 0) {
				throw new ArgumentException("Name must have at least one part.", nameof(name));
			}
			if(name.Length == 1) {
				this.Identifier = new IdentifierNameExpression(name[0]);
			}else {
				this.Identifier = new MultiPartIdentifierNameExpression(name);
			}
		}
		
		public bool Nullable { get; init; }
		public IIdentifierNameExpression Identifier { get; init; }
		public override IEnumerable<ISyntaxNode> Children => [Identifier];
		public override TextWriter Generate(TextWriter writer) => writer.Code(Identifier).AppendIf(Nullable, "?");
	}
}