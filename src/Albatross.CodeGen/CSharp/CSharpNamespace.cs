using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp
{
	[CodeGenerator("csharp.namespace", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# Namespace") ]
	public class CSharpNamespace : ICodeGenerator<object, CSharpClassOption> {
		public int TabLevel { get; set; }

		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Generate(StringBuilder sb, object source, CSharpClassOption option) {
			HashSet<string> imports = new HashSet<string> {
				"System"
			};
			if (option.Imports != null) { imports.AddRange(option.Imports); }
			foreach (var ns in imports) {
				sb.Append("using ").Append(ns).Terminate();
			}
			option.Imports = new string[0];
			sb.Tab(TabLevel).Append("namespace ").Append(option.Namespace).OpenScope();
			option.Namespace = null;
			StringBuilder scoped = new StringBuilder();
			Yield?.Invoke(scoped);
			sb.Tabify(scoped.ToString(), TabLevel + 1);
			sb.CloseScope();

			return new object[] { this, };
		}

		public IEnumerable<object> Generate(StringBuilder sb, object source, ICodeGeneratorOption option) {
			return this.ValidateNGenerate(sb, source, option);
		}

		public void Configure(object data) {
		}
	}
}
