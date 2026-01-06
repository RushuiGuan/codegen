using Albatross.CodeGen.CSharp.Expressions;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class FileDeclaration : CodeNode, IDeclaration {
		public FileDeclaration(string name) {
			this.Name = name;
		}

		public string FileName => $"{Name}.cs";
		public string Name { get; }
		public bool NullableEnabled { get; init; } = true;

		public NamespaceExpression? Namespace { get; init; }
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
			if (Namespace != null) {
				using (var scope = writer.Code(Defined.Keywords.Namespace).Code(Namespace).BeginScope()) {
					OmitCode(scope.Writer);
				}
			} else {
				OmitCode(writer);
			}
			return writer;
		}

		void OmitCode(TextWriter writer) {
			foreach (var item in Attributes) {
				writer.Code(item).WriteLine();
			}
			foreach (var item in Interfaces) {
				writer.Code(item).WriteLine();
			}
			foreach (var item in Classes) {
				writer.Code(item).WriteLine();
			}
		}

		public override IEnumerable<ICodeNode> Children {
			get {
				var list = new List<ICodeNode>(Imports);
				list.AddRange(Attributes);
				list.AddRange(Classes);
				list.AddRange(Interfaces);
				return list;
			}
		}
	}
}