using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Declarations {
	public record class ConstructorDeclaration : MethodDeclaration {
		public ConstructorDeclaration() : base("constructor") {
			ReturnType = Defined.Types.Void();
		}
		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier).OpenParenthesis().Code(Parameters).CloseParenthesis();
			writer.Code(Body);
			return writer;
		}
	}
}