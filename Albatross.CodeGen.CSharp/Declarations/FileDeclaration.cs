using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class FileDeclaration : IDeclaration {
		public FileDeclaration(string name) {
			this.Name = name;
		}

		public string FileName => $"{Name}.cs";
		public string Name { get; }
		public bool NullableEnabled { get; init; } = false;

		public required NamespaceExpression Namespace { get; init; }
		public IEnumerable<ImportExpression> Imports { get; init; } = [];
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public IEnumerable<ClassDeclaration> Classes { get; init; } = [];
		public IEnumerable<InterfaceDeclaration> Interfaces { get; init; } = [];

		public TextWriter Generate(TextWriter writer) {
			foreach (var item in Imports.OrderBy(x => x.Namespace.Source)) {
				writer.Code(item).WriteLine();
			}
			writer.WriteLine();
			if(NullableEnabled) {
				writer.Code(Defined.PreprocessorDirectives.NullableEnable).WriteLine();
			}
			using var scope = writer.Code(Defined.Keywords.Namespace).Code(Namespace).BeginScope();
			foreach (var item in Attributes) {
				scope.Writer.Code(item).WriteLine();
			}
			foreach (var item in Interfaces) {
				scope.Writer.Code(item).WriteLine();
			}
			foreach (var item in Classes) {
				scope.Writer.Code(item).WriteLine();
				//TODO: delete after verification
				scope.Writer.WriteLine();
			}
			//TODO: delete after verification
			if (NullableEnabled) {
				writer.Code(Defined.PreprocessorDirectives.NullableDisable).WriteLine();
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>(Imports);
			list.AddRange(Attributes);
			list.AddRange(Classes);
			list.AddRange(Interfaces);
			return list;
		}
	}
}