using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Python.Declarations {
	public record class ConstructorDeclaration : MethodDeclaration {
		public ConstructorDeclaration() : base("__init__") {
			ReturnType = Defined.Types.None;
		}
	}
}