using Albatross.CodeAnalysis;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using SymbolFilter = Albatross.CodeGen.WebClient.Settings.SymbolFilter;

namespace Albatross.CodeGen.WebClient {
	public class ApiControllerClassWalker : CSharpSyntaxWalker {
		private readonly SemanticModel semanticModel;
		private readonly IEnumerable<SymbolFilter> filters;
		public List<INamedTypeSymbol> Result { get; } = new List<INamedTypeSymbol>();

		public ApiControllerClassWalker(SemanticModel semanticModel, IEnumerable<SymbolFilter> filters) {
			this.semanticModel = semanticModel;
			this.filters = filters;
		}
		public override void VisitClassDeclaration(ClassDeclarationSyntax node) {
			if (node.Identifier.Text.EndsWith("Controller")) {
				var symbol = semanticModel.GetDeclaredSymbol(node);
				if (symbol?.BaseType != null && symbol.BaseType.Name == "ControllerBase") {
					if (filters.ShouldKeep(symbol.GetFullName())) {
						Result.Add(symbol);
					}
				}
			}
			base.VisitClassDeclaration(node);
		}
	}
}