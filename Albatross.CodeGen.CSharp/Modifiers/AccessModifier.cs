using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.CSharp.Modifiers {
	public record class AccessModifier : IModifier {
		public AccessModifier(string name) {
			Name = name;
		}

		public static AccessModifier Public = new AccessModifier("public");
		public static AccessModifier Private = new AccessModifier("private");
		public static AccessModifier Protected = new AccessModifier("protected");
		public static AccessModifier Internal = new AccessModifier("internal");
		public string Name { get; }
	}
}