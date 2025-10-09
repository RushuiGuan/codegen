using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Python.Declarations {
	public record class ConstructorDeclaration : MethodDeclaration {
		public ConstructorDeclaration() : base("__init__") {
			ReturnType = Defined.Types.None();
		}
		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier).OpenParenthesis().Code(Parameters).CloseParenthesis();
			using (var scope = writer.BeginScope()) {
				scope.Writer.Code(Body);
			}
			writer.WriteLine();
			return writer;
		}
	}
}