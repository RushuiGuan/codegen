using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ModuleSourceExpression : CodeNode, ISourceExpression {
		public static readonly Regex ModuleSource = new Regex(@"^(?:\.+)?(?:[a-z_][a-z0-9_]*)(?:\.[a-z_][a-z0-9_]*)*$",
			RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.IgnoreCase);

		// this regex is more restrictive than the actual module name regex
		public ModuleSourceExpression(string name) {
			if (ModuleSource.IsMatch(name)) {
				this.Source = name;
			} else {
				throw new ArgumentException($"Invalid module name {name}");
			}
		}
		public string Source { get; }
		public override TextWriter Generate(TextWriter writer) => writer.Append(Source);
		public override IEnumerable<ICodeNode> Children => Array.Empty<ICodeNode>();
	}
}