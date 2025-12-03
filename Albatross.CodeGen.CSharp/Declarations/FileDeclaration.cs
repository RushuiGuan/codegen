using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class FileDeclaration : IDeclaration, ISyntaxNode{
		public FileDeclaration(string name) {
			this.Name = name;
		}
		public string FileName => $"{Name}.cs";
		public string Name { get; }
		
		public IEnumerable<UsingExpression> UsingExpressions { get; init; } = [];
		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		public IEnumerable<ClassDeclaration> ClassDeclarations { get; init; } = [];
		public TextWriter Generate(TextWriter writer) {
			foreach(var item in UsingExpressions){
				writer.Code(item).WriteLine();
			}
			foreach(var item in AttributeExpressions){
				writer.Code(item).WriteLine();
			}
			foreach (var item in ClassDeclarations) {
				writer.Code(item).WriteLine();
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>(UsingExpressions);
			list.AddRange(AttributeExpressions);
			list.AddRange(ClassDeclarations);
			return list;
		}
	}
}