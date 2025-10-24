﻿using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class PythonFileDeclaration : SyntaxNode, IDeclaration {
		public PythonFileDeclaration(string name) {
			this.Name = name;
		}
		public string FileName => $"{Name}.py";
		public string Name { get; }
		public IEnumerable<ImportExpression> ImportDeclarations { get; init; } = [];
		public IEnumerable<ClassDeclaration> ClasseDeclarations { get; init; } = [];

		public override IEnumerable<ISyntaxNode> Children => ImportDeclarations.Cast<ISyntaxNode>()
			.Union(ClasseDeclarations);

		bool IsSelf(ISourceExpression source) {
			if (source is ModuleSourceExpression moduleSourceExpression && moduleSourceExpression.ModuleName == this.Name) {
				return true;
			} else {
				return false;
			}
		}

		public override TextWriter Generate(TextWriter writer) {
			var importExpressions = this.ImportDeclarations
				.Union(new ImportCollection(this.GetDescendants()))
				.Where(x => !IsSelf(x.Source));
			new ImportCollection(importExpressions).Generate(writer);
			writer.WriteLine();
			foreach (var item in ClasseDeclarations) {
				writer.Code(item);
			}
			return writer;
		}
	}
}