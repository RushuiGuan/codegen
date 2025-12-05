using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public class PreprocessorDirectiveExpression : ICodeElement{
		private readonly string text;

		public PreprocessorDirectiveExpression(string text) {
			this.text = text;
		}

		public TextWriter Generate(TextWriter writer) {
			writer.Append("#").Append(text);
			return writer;
		}
	}
}