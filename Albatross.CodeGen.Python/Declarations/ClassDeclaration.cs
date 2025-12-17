using Albatross.CodeGen.Python.Expressions;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class ClassDeclaration : CodeNode, IDeclaration {
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
		public DocStringExpression? DocString { get; init; }

		public override IEnumerable<ICodeNode> Children
			=> new List<ICodeNode> { Identifier, }
				.AddIfNotNull(BaseClassName)
				.AddIfNotNull(Constructor)
				.Concat(Decorators).Concat(Imports).Concat(Fields).Concat(GetProperties).Concat(Methods);

		public override TextWriter Generate(TextWriter writer) {
			Decorators.ForEach(x => writer.Code(x).AppendLine());
			writer.Append("class ").Code(Identifier);
			if (BaseClassName != null) {
				writer.Append("(").Code(BaseClassName).Append(")");
			}
			using (var scope = writer.BeginPythonScope()) {
				bool pass = true;
				if (DocString != null) {
					pass = false;
					scope.Writer.Code(DocString).AppendLine();
				}
				foreach (var field in Fields) {
					pass = false;
					scope.Writer.Code(field).AppendLine();
				}
				foreach (var getter in GetProperties) {
					pass = false;
					scope.Writer.Code(getter);
				}
				if (Constructor != null) {
					pass = false;
					scope.Writer.Code(Constructor);
				}
				foreach (var method in Methods) {
					pass = false;
					scope.Writer.Code(method);
				}
				if (pass) {
					scope.Writer.Append("pass");
				}
			}
			writer.WriteLine();
			return writer;
		}
	}
}