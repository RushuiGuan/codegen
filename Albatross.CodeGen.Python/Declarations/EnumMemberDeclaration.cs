using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.Python.Declarations {
	public record class EnumMemberDeclaration: FieldDeclaration {
		public EnumMemberDeclaration(string name, IExpression expression) : base(name) {
			this.Initializer = expression;
		}
	}
}