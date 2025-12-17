using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class GenericSourceExpression : CodeNode, ISourceExpression {
		public GenericSourceExpression(string name) {
			Source = name;
		}
		public string Source { get; }
		public override TextWriter Generate(TextWriter writer) {
			writer.Append('"').Append(Source).Append('"');
			return writer;
		}
	}
}