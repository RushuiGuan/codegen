using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	/// <summary>
	/// Represents a C# class declaration with support for modifiers, generics, inheritance, and members
	/// </summary>
	public record class ClassDeclaration : CodeNode, IDeclaration {
		/// <summary>
		/// Gets or sets the access modifier for the class (public, private, protected, internal)
		/// </summary>
		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Public;
		/// <summary>
		/// Gets or sets the name of the class
		/// </summary>
		public required IdentifierNameExpression Name { get; init; }
		/// <summary>
		/// Gets or sets the base types (base class and interfaces) that this class inherits from or implements
		/// </summary>
		public IEnumerable<ITypeExpression> BaseTypes { get; init; } = [];
		/// <summary>
		/// Gets or sets the generic type arguments for the class
		/// </summary>
		public ListOfGenericArguments ListOfGenericArguments { get; init; } = new();
		/// <summary>
		/// Gets or sets a value indicating whether the class is static
		/// </summary>
		public bool IsStatic { get; init; }
		/// <summary>
		/// Gets or sets a value indicating whether the class is sealed
		/// </summary>
		public bool IsSealed { get; init; }
		/// <summary>
		/// Gets or sets a value indicating whether the class is abstract
		/// </summary>
		public bool IsAbstract { get; init; }
		/// <summary>
		/// Gets or sets a value indicating whether the class is partial
		/// </summary>
		public bool IsPartial { get; init; }
		/// <summary>
		/// Gets or sets a value indicating whether the class is declared as a record
		/// </summary>
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
			if (IsSealed) { writer.Code(Defined.Keywords.Sealed); }
			if (IsRecord) { writer.Code(Defined.Keywords.Record); }
			if (IsPartial) { writer.Code(Defined.Keywords.Partial); }
			if (IsAbstract) { writer.Code(Defined.Keywords.Abstract); }
			writer.Code(Defined.Keywords.Class).Code(Name).Code(ListOfGenericArguments);
			writer.WriteItems(BaseTypes, ", ", (w, args) => w.Code(args), " : ");
			using (var scope = writer.BeginScope()) {
				foreach (var constructor in Constructors) {
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

		public override IEnumerable<ICodeNode> Children {
			get {
				var list = new List<ICodeNode> { ListOfGenericArguments };
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