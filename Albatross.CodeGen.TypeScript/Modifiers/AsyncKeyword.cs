using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.TypeScript.Modifiers {
	public record class AsyncKeyword : Keyword {
		public AsyncKeyword() : base("async") { }
	}
}