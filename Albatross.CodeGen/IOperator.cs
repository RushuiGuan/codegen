using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// Interface for operators used in expressions (e.g., +, -, *, ==, etc.)
	/// </summary>
	public interface IOperator : ICodeElement {
		/// <summary>
		/// Gets the operator name or symbol
		/// </summary>
		public string Name { get; }
	}
	
	/// <summary>
	/// Concrete implementation of an operator with automatic spacing
	/// </summary>
	public record class Operator : IOperator {
		/// <summary>
		/// Initializes a new instance of the Operator class
		/// </summary>
		/// <param name="name">The operator symbol or name</param>
		public Operator(string name) {
			Name = name;
		}
		
		/// <summary>
		/// Gets the operator symbol or name
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// Generates the operator code with proper spacing
		/// </summary>
		/// <param name="writer">The TextWriter to write the generated code to</param>
		/// <returns>The TextWriter for method chaining</returns>
		public TextWriter Generate(TextWriter writer) => writer.Append($" {Name} ");
	}
}