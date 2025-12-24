using Albatross.CodeGen.CSharp.Expressions;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ParameterDeclaration : CodeNode, IDeclaration {
		public ITypeExpression? Type { get; init; }
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public required IdentifierNameExpression Name { get; init; }
		public bool UseThisKeyword { get; init; }

		[MemberNotNullWhen(false, nameof(Type))]
		public bool IsTypeInferred => Type == null || Type.Equals(Defined.Types.Var);

		public override TextWriter Generate(TextWriter writer) {
			foreach (var attrib in Attributes) {
				writer.Code(attrib);
			}
			if (UseThisKeyword) {
				writer.Code(Defined.Keywords.This);
			}
			if (!IsTypeInferred) {
				writer.Code(Type).Space();
			}
			writer.Code(Name);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => Attributes.Cast<ICodeNode>().Concat([Name]).UnionIfNotNull(Type);
	}
}