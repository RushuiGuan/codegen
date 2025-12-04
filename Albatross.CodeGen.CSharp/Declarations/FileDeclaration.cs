using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class FileDeclaration : IDeclaration{
		public FileDeclaration(string name) {
			this.Name = name;
		}
		public string FileName => $"{Name}.cs";
		public string Name { get; }

		public IEnumerable<UsingExpression> Imports { get; init; } = [];
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public IEnumerable<ClassDeclaration> Classes { get; init; } = [];
		public IEnumerable<InterfaceDeclaration> Interfaces { get; init; } = [];
	
		public TextWriter Generate(TextWriter writer) {
			foreach(var item in Imports){
				writer.Code(item).WriteLine();
			}
			foreach(var item in Attributes){
				writer.Code(item).WriteLine();
			}
			foreach (var item in Interfaces) {
				writer.Code(item).WriteLine();
			}
			foreach (var item in Classes) {
				writer.Code(item).WriteLine();
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