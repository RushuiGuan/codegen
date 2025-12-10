using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class FileDeclaration : SyntaxNode, IDeclaration {
		public FileDeclaration(string name) {
			this.Name = name;
		}

		public string FileName => $"{Name}.cs";
		public string Name { get; }
		public bool NullableEnabled { get; init; } = true;

		public required NamespaceExpression Namespace { get; init; }
		public IEnumerable<ImportExpression> Imports { get; init; } = [];
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public IEnumerable<ClassDeclaration> Classes { get; init; } = [];
		public IEnumerable<InterfaceDeclaration> Interfaces { get; init; } = [];

		public override TextWriter Generate(TextWriter writer) {
			var importCollection = new ImportCollection(Imports, GetDescendants());
			writer.Code(importCollection);
			if (NullableEnabled) {
				writer.Code(Defined.PreprocessorDirectives.NullableEnable).WriteLine();
			}
			using (var scope = writer.Code(Defined.Keywords.Namespace).Code(Namespace).BeginScope()) {
				foreach (var item in Attributes) {
					scope.Writer.Code(item).WriteLine();
				}
				foreach (var item in Interfaces) {
					scope.Writer.Code(item).WriteLine();
				}
				foreach (var item in Classes) {
					scope.Writer.Code(item).WriteLine();
				}
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var list = new List<ISyntaxNode>(Imports);
				list.AddRange(Attributes);
				list.AddRange(Classes);
				list.AddRange(Interfaces);
				return list;
			}
		}
	}
}