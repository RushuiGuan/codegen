using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.TypeScript.Modifiers {
	public record class AccessKeyword : Keyword {
		public AccessKeyword(string name) : base(name) { }
	}
}