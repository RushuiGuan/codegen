using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	/// <summary>
	/// Represents a C# type expression with support for generics and nullability
	/// </summary>
	public record class TypeExpression : CodeNode, ITypeExpression {
		/// <summary>
		/// Initializes a new instance of the TypeExpression class with a type name and optional generic arguments
		/// </summary>
		/// <param name="name">The type name</param>
		/// <param name="genericArguments">Optional generic type arguments</param>
		public TypeExpression(string name, params string[] genericArguments) {
			this.Identifier = new IdentifierNameExpression(name);
			this.GenericArguments = genericArguments.Select(arg => new TypeExpression(arg));
		}

		/// <summary>
		/// Initializes a new instance of the TypeExpression class with an identifier and optional generic arguments
		/// </summary>
		/// <param name="identifier">The type identifier</param>
		/// <param name="genericArguments">Optional generic type expressions</param>
		public TypeExpression(IIdentifierNameExpression identifier, params IEnumerable<ITypeExpression> genericArguments) {
			this.Identifier = identifier;
			this.GenericArguments = genericArguments;
		}

		/// <summary>
		/// Gets or sets whether this type is nullable
		/// </summary>
		public bool Nullable { get; init; }
		
		/// <summary>
		/// Gets a value indicating whether this type has generic arguments
		/// </summary>
		public bool IsGeneric => GenericArguments.Any();
		
		/// <summary>
		/// Gets the generic type arguments for this type
		/// </summary>
		public IEnumerable<ITypeExpression> GenericArguments { get; init; } = [];
		
		/// <summary>
		/// Gets the identifier that represents the type name
		/// </summary>
		public IIdentifierNameExpression Identifier { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Identifier)
				.WriteItems(GenericArguments, ",", (w, args) => w.Code(args), "<", ">")
				.AppendIf(Nullable, "?");
		}

		public override IEnumerable<ICodeNode> Children =>
			new List<ICodeNode>(GenericArguments) {
				Identifier
			};
	}
}