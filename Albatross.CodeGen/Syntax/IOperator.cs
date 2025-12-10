using Albatross.Text;
using System;
using System.IO;

namespace Albatross.CodeGen.Syntax {
	public interface IOperator : ICodeElement {
		public string Name { get; }
	}
	public record class Operator : IOperator {
		public Operator(string name) {
			Name = name;
		}
		public string Name { get; }
		public TextWriter Generate(TextWriter writer) => writer.Append($" {Name} ");
	}
}