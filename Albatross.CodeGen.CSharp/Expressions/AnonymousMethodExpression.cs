using Albatross.CodeGen.CSharp.Declarations;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class AnonymousMethodExpression : CodeNode, IExpression {
		public ListOfParameterDeclarations Parameters { get; } = new ListOfParameterDeclarations();
		public CodeBlock Body { get; } = new CSharpCodeBlock();
		public override TextWriter Generate(TextWriter writer) {
			if (Parameters.Count() == 1 && Parameters.First().IsTypeInferred) {
				// omit parantheses and type for single var parameter
				writer.Code(Parameters.First().Name);
			} else {
				writer.Code(Parameters);
			}
			writer.Append(" =>");
			if (Body.Count() <= 1) {
				writer.Space();
			}
			if (Body.Count() == 1) {
				writer.Code(Body.First());
			} else {
				writer.Code(Body);
			}
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => [Parameters, Body];
	}
}