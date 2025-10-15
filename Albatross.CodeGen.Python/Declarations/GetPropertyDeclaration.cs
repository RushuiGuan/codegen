using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.Python.Declarations {
	public record class GetPropertyDeclaration : MethodDeclaration {
		public GetPropertyDeclaration(string name) : base(name) {
			this.Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(
				new ParameterDeclaration {
					Identifier = Defined.Identifiers.Self,
					Type = Defined.Types.None,
				});
			this.Decorators = [Defined.Decorators.Property];
		}
	}
}