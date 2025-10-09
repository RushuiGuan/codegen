using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.TypeScript.Modifiers {
	public record class AsyncModifier : IModifier {
		public string Name { get; } = "async";
	}
}