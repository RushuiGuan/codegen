using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Declarations {
	public record class ClassDeclaration : SyntaxNode, IDeclaration {
		public ClassDeclaration(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}

		public IdentifierNameExpression Identifier { get; }
		public IIdentifierNameExpression? BaseClassName { get; init; }
		public IEnumerable<FieldDeclaration> Fields { get; init; } = [];
		public IEnumerable<GetPropertyDeclaration> GetProperties { get; init; } = [];

		public InvocationExpression[] Decorators { get; init; } = [];
		public MethodDeclaration? Constructor { get; init; }
		public IEnumerable<ImportExpression> Imports { get; init; } = [];
		public IEnumerable<MethodDeclaration> Methods { get; init; } = [];

		public override IEnumerable<ISyntaxNode> Children
			=> new List<ISyntaxNode> { Identifier, }
				.AddIfNotNull(BaseClassName)
				.AddIfNotNull(Constructor)
				.UnionAll(Decorators, Imports, Fields, GetProperties, Methods);

		public override TextWriter Generate(TextWriter writer) {
			Decorators.ForEach(x => writer.Code(x).AppendLine());
			writer.Append("class ").Code(Identifier);
			if (BaseClassName != null) {
				writer.Append("(").Code(BaseClassName).Append(")");
			}
			using (var scope = writer.BeginPythonScope()) {
				foreach(var field in Fields) {
					scope.Writer.Code(field).AppendLine();
				}
				foreach (var getter in GetProperties) {
					scope.Writer.Code(getter);
				}
				if (Constructor != null) {
					scope.Writer.Code(Constructor);
				}
				foreach (var method in Methods) {
					scope.Writer.Code(method);
				}
			}
			writer.WriteLine();
			return writer;
		}
	}
}