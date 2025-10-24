using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.Python.Modifiers {
	public record class AsyncModifier : IModifier {
		public string Name { get; } = "async";
	}
}