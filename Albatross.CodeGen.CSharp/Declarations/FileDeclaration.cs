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

		public List<UsingExpression> UsingExpressions { get; init; } = new();
		public List<AttributeExpression> AttributeExpressions { get; init; } = new();
		public List<ClassDeclaration> ClassDeclarations { get; init; } = new();
		public List<InterfaceDeclaration> InterfaceDeclarations { get; init; } = new();
		public TextWriter Generate(TextWriter writer) {
			foreach(var item in UsingExpressions){
				writer.Code(item).WriteLine();
			}
			foreach(var item in AttributeExpressions){
				writer.Code(item).WriteLine();
			}
			foreach (var item in InterfaceDeclarations) {
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
			list.AddRange(InterfaceDeclarations);
			return list;
		}
	}
}