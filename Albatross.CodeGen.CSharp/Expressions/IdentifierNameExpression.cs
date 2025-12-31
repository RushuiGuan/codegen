using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.CSharp.Expressions {
	/// <summary>
	/// Represents a C# identifier name expression with optional generic arguments and validation
	/// </summary>
	public record class IdentifierNameExpression : CodeNode, IIdentifierNameExpression {
		/// <summary>
		/// Regular expression pattern for validating C# identifier names
		/// </summary>
		public static readonly Regex IdentifierName = new Regex(@"^
			(?:[a-z_][a-z0-9_]*)
			(?:\.[a-z_][a-z0-9_]*)*
			$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		/// <summary>
		/// Initializes a new instance of the IdentifierNameExpression class with validation
		/// </summary>
		/// <param name="name">The identifier name to validate and use</param>
		/// <exception cref="ArgumentException">Thrown when the name is not a valid C# identifier</exception>
		public IdentifierNameExpression(string name) {
			if (IdentifierName.IsMatch(name)) {
				this.Name = name;
			} else {
				throw new ArgumentException($"Invalid identifier name '{name}'");
			}
		}
		
		/// <summary>
		/// Gets the validated identifier name
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// Gets or sets the generic arguments for this identifier
		/// </summary>
		public ListOfGenericArguments GenericArguments { get; init; } = new();
		public override TextWriter Generate(TextWriter writer) => writer.Append(Name).Code(GenericArguments);
		public override IEnumerable<ICodeNode> Children => [GenericArguments];
	}
}