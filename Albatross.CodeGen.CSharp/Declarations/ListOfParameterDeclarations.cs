using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ListOfParameterDeclarations : ListOfSyntaxNodes<ParameterDeclaration> {
		public override TextWriter Generate(TextWriter writer) {
			writer.Append("(");
			base.Generate(writer);
			writer.Append(")");
			return writer;
		}
	}
}