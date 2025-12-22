using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// Represents a no-operation expression that generates no output when rendered
	/// </summary>
	public record NoOpExpression : IExpression {
		/// <summary>
		/// Generates no output and returns the writer unchanged
		/// </summary>
		/// <param name="writer">The TextWriter to write to</param>
		/// <returns>The unchanged TextWriter</returns>
		public TextWriter Generate(TextWriter writer) => writer;
		
		/// <summary>
		/// Returns an empty collection since this expression has no descendants
		/// </summary>
		/// <returns>An empty enumerable of ICodeNode</returns>
		public IEnumerable<ICodeNode> GetDescendants() => Array.Empty<ICodeNode>();
	}
}