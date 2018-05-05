using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp
{
	[CodeGenerator("csharp.namespace", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# Namespace") ]
	public class CSharpNamespace : ICodeGenerator<object, CSharpClassOption> {
		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Generate(StringBuilder sb, object source, CSharpClassOption option) {
			HashSet<string> imports = new HashSet<string>();
			imports.Add("System");
			if (option.Imports != null) { imports.AddRange(option.Imports); }
			foreach (var ns in imports) {
				sb.Append("using ").Append(ns).Terminate();
			}
			option.Imports = new string[0];
			sb.Tab(option.TabLevel).Append("namespace ").Append(option.Namespace).OpenScope();
			option.TabLevel++;
			option.Namespace = null;
			Yield?.Invoke(sb);
			sb.CloseScope();

			return new object[] { this, };
		}

		public IEnumerable<object> Generate(StringBuilder sb, object source, object option) {
			return this.ValidateNBuild(sb, source, option);
		}

		public void Configure(object data) {
		}
	}
}
