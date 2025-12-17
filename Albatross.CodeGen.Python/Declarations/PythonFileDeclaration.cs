using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class PythonFileDeclaration : CodeNode, IDeclaration {
		public PythonFileDeclaration(string name) {
			this.Name = name;
		}
		public string FileName => $"{Name}.py";
		public string Name { get; }
		public IEnumerable<CommentDeclaration> Banner { get; init; } = [];
		public IEnumerable<ImportExpression> Imports { get; init; } = [];
		public IEnumerable<ClassDeclaration> Classes { get; init; } = [];

		public override IEnumerable<ICodeNode> Children => Imports.Cast<ICodeNode>()
			.Union(Classes);

		bool IsSelf(ISourceExpression source) {
			if (source is ModuleSourceExpression moduleSourceExpression && (moduleSourceExpression.Source.TrimStart('.') == this.Name)) {
				return true;
			} else {
				return false;
			}
		}

		public override TextWriter Generate(TextWriter writer) {
			foreach (var item in Banner) {
				writer.Code(item);
			}
			writer.WriteLine();
			var importExpressions = this.Imports
				.Union(new ImportCollection(this.GetDescendants().OfType<QualifiedIdentifierNameExpression>()).Imports)
				.Where(x => !IsSelf(x.Source));
			new ImportCollection(importExpressions).Generate(writer);
			writer.WriteLine();
			foreach (var item in Classes) {
				writer.Code(item);
			}
			return writer;
		}
	}
}