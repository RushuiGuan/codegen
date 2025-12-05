using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class GenericIdentifierNameExpression : IdentifierNameExpression {
		public GenericIdentifierNameExpression(string name) : base(name) { }
		public required ListOfGenericArguments GenericArguments { get; init; }
		public override TextWriter Generate(TextWriter writer) => writer.Append(Name).Code(GenericArguments);
		public override IEnumerable<ISyntaxNode> GetDescendants() => [GenericArguments];
	}
}