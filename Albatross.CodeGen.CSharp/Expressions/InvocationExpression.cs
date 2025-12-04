using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class InvocationExpression : ISyntaxNode, IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IExpression CallableExpression { get; init; }
		public List<ITypeExpression> GenericArguments { get; init; } = new();
		public List<IExpression> ArgumentList { get; init; } = new();

		public virtual TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) { writer.Append("await "); }
			writer.Code(CallableExpression);
			writer.WriteItems(GenericArguments, ", ", (w, args)=> w.Code(args), prefix: "<", postfix: ">");
			writer.WriteItems(ArgumentList, ", ", (w, arg) => w.Code(arg), prefix: "(", postfix: ")");
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>{
				CallableExpression
			};
			list.AddRange(GenericArguments);
			list.AddRange(ArgumentList);
			return list;
		}
	}
}