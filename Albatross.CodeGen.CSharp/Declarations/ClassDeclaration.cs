using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ClassDeclaration : SyntaxNode, IDeclaration {
		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Public;
		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<ITypeExpression> BaseTypes { get; init; } = [];
		public ListOfGenericArguments ListOfGenericArguments { get; init; } = new();
		public bool IsStatic { get; init; }
		public bool IsSealed { get; init; }
		public bool IsAbstract { get; init; }
		public bool IsPartial { get; init; }
		public bool IsRecord { get; init; }

		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public IEnumerable<ConstructorDeclaration> Constructors { get; init; } = [];
		public IEnumerable<PropertyDeclaration> Properties { get; init; } = [];
		public IEnumerable<FieldDeclaration> Fields { get; init; } = [];
		public IEnumerable<MethodDeclaration> Methods { get; init; } = [];

		public override TextWriter Generate(TextWriter writer) {
			foreach (var attribute in Attributes) {
				writer.Code(attribute).WriteLine();
			}
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			if (IsStatic) { writer.Code(Defined.Keywords.Static); }
			if (IsSealed) { writer.Code(Defined.Keywords.Sealed);}
			if (IsRecord) { writer.Code(Defined.Keywords.Record);}
			if (IsPartial) { writer.Code(Defined.Keywords.Partial); }
			if(IsAbstract) { writer.Code(Defined.Keywords.Abstract); }
			writer.Code(Defined.Keywords.Class).Code(Name).Code(ListOfGenericArguments);
			writer.WriteItems(BaseTypes, ", ", (w, args) => w.Code(args), " : ", null);
			using (var scope = writer.BeginScope()) {
				foreach(var constructor in Constructors) {
					scope.Writer.Code(constructor).WriteLine();
				}
				foreach (var field in Fields) {
					scope.Writer.Code(field).WriteLine();
				}
				foreach (var property in Properties) {
					scope.Writer.Code(property).WriteLine();
				}
				foreach (var method in Methods) {
					scope.Writer.Code(method).WriteLine();
				}
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var list = new List<ISyntaxNode> { ListOfGenericArguments };
				list.AddRange(Constructors);
				list.AddRange(Fields);
				list.AddRange(Properties);
				list.AddRange(Methods);
				list.AddRange(BaseTypes);
				return list;
			}
		}
	}
}