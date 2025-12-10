using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class AnonymousMethodExpression : SyntaxNode, IExpression {
		public IEnumerable<ParameterDeclaration> Parameters { get; init; } = Array.Empty<ParameterDeclaration>();
		public IEnumerable<IExpression> Body { get; init; } = [];
		public override TextWriter Generate(TextWriter writer) {
			if (!Parameters.Any()) {
				writer.Append("()");
			} else if (Parameters.Count() == 1 && Object.Equals(Parameters.First().Type, Defined.Types.Var)) {
				// omit parantheses and type for single var parameter
				writer.Code(Parameters.First().Name);
			} else {
				writer.WriteItems(Parameters, ", ", (w, param) => {
					if (object.Equals(param.Type, Defined.Types.Var)) {
						w.Code(param.Name);
					} else {
						w.Code(param);
					}
				}, "(", ")");
			}
			writer.Append(" =>");
			if (!Body.Any()) {
				writer.Append(" { }");
			} else if (Body.Count() == 1) {
				writer.Space().Code(Body.First());
			} else {
				using var scope = writer.BeginScope();
				scope.Writer.Code(new CodeBlock(Body));
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var list = new List<ISyntaxNode>(Parameters);
				list.AddRange(Body);
				return list;
			}
		}
	}
}