using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class MethodDeclaration : IDeclaration {
		public bool IsAbstract { get; init; }
		public bool IsVirtual { get; init; }
		public bool IsPartial { get; init; }

		public required IdentifierNameExpression Name { get; init; }
		public required ITypeExpression ReturnType { get; init; }
		public ListOfGenericArguments GenericArguments { get; init; } = new();
		public AccessModifierKeyword? AccessModifier { get; init; }

		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public ListOfParameterDeclarations Parameters { get; init; } = new();
		public IExpression? Body { get; init; }
		public bool IsAsync { get; init; }
		public bool IsStatic { get; set; }

		public TextWriter Generate(TextWriter writer) {
			if (AccessModifier != null) { writer.Append(AccessModifier.Name).Space(); }
			if (IsStatic) { writer.Code(Defined.Keywords.Static); }
			if (IsAbstract) { writer.Code(Defined.Keywords.Abstract); }
			if (IsVirtual) { writer.Code(Defined.Keywords.Virtual); }
			if (IsPartial) { writer.Code(Defined.Keywords.Partial); }
			if (IsAsync) { writer.Code(Defined.Keywords.Async); }
			writer.Code(ReturnType).Space().Code(Name).Code(GenericArguments);
			writer.Code(Parameters);
			if (Body != null) {
				using var scope = writer.BeginScope();
				scope.Writer.Code(Body);
			} else {
				writer.Semicolon();
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>{
				Parameters, ReturnType, Name, GenericArguments
			}.AddIfNotNull(Body);
			list.AddRange(Attributes);
			return list;
		}
	}
}