using Albatross.CodeAnalysis;
using Albatross.CodeGen.SymbolProviders;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SymbolFilter = Albatross.CodeGen.WebClient.Settings.SymbolFilter;

namespace Albatross.CodeGen.WebClient {
	/// <summary>
	/// Walks the syntax tree to find DTO classes and enum types for code generation.
	/// Filters out abstract classes, static classes, interfaces, delegates, and JsonConverter derivatives.
	/// </summary>
	public class DtoClassEnumWalker : CSharpSyntaxWalker {
		private readonly SemanticModel semanticModel;
		public List<INamedTypeSymbol> DtoClasses { get; } = new List<INamedTypeSymbol>();
		public List<INamedTypeSymbol> EnumTypes { get; } = new List<INamedTypeSymbol>();
		readonly SymbolFilter[] filters;

		public DtoClassEnumWalker(SemanticModel semanticModel, SymbolFilter[] filters) {
			this.semanticModel = semanticModel;
			this.filters = filters;
		}

		bool IsValidDtoClass([NotNullWhen(true)] INamedTypeSymbol? symbol) =>
			symbol is { DeclaredAccessibility: Accessibility.Public, IsAbstract: false, IsAnonymousType: false, IsStatic: false }
			&& symbol.TypeKind != TypeKind.Delegate
			&& !symbol.IsGenericTypeDefinition()
			&& symbol.TypeKind != TypeKind.Interface
			&& symbol.TypeKind != TypeKind.Enum
			&& !symbol.IsDerivedFrom(this.semanticModel.Compilation.JsonConverterClass());

		public override void VisitClassDeclaration(ClassDeclarationSyntax node) {
			var symbol = semanticModel.GetDeclaredSymbol(node);
			if (IsValidDtoClass(symbol) && this.filters.ShouldKeep(symbol.GetFullName())) {
				DtoClasses.Add(symbol);
			}
			base.VisitClassDeclaration(node);
		}

		public override void VisitRecordDeclaration(RecordDeclarationSyntax node) {
			var symbol = semanticModel.GetDeclaredSymbol(node);
			if (IsValidDtoClass(symbol) && this.filters.ShouldKeep(symbol.GetFullName())) {
				DtoClasses.Add(symbol);
			}
			base.VisitRecordDeclaration(node);
		}
		public override void VisitEnumDeclaration(EnumDeclarationSyntax node) {
			var symbol = semanticModel.GetDeclaredSymbol(node);
			if (symbol != null && symbol.DeclaredAccessibility == Accessibility.Public && filters.ShouldKeep(symbol.GetFullName())) {
				EnumTypes.Add(symbol);
			}
			base.VisitEnumDeclaration(node);
		}
	}
}