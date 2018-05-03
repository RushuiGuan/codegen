using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.CSharp {
	/// <summary>
	/// ClassGenerator is an abstract class for C# class code generation.  It uses a fixed option type <see cref="Albatross.CodeGen.CSharp.ClassOption" /> but the Source Type is left open.  It will take care of 
	/// namespace, class scope, class name and constructor generation using the information provided by the <see cref="Albatross.CodeGen.CSharp.ClassOption" /> object.  It leaves the rest of the body creation to a abstract 
	/// method <see cref="Albatross.CodeGen.CSharp.CSharpClassGenerator{T}.RenderBody(StringBuilder, int, T, CSharpClassOption)"/>.
	/// </summary>
	/// <typeparam name="T">the source type</typeparam>
	public abstract class CSharpClassGenerator<T> : ICodeGenerator<T, CSharpClassOption> where T : class {

#pragma warning disable 0067
		public event Func<StringBuilder, IEnumerable<object>> Yield;
#pragma warning restore 0067

		/// <summary>
		/// Return the name of the class.  By default, it will use the <see cref="Albatross.CodeGen.CSharp.ClassOption.Name"/> property.  Override this method to change the behavior.
		/// </summary>
		/// <param name="t">Source input</param>
		/// <param name="option">Option input</param>
		/// <returns>The name for this class</returns>
		public virtual string GetClassName(T t, CSharpClassOption option) {
			return option.Name;
		}
		public abstract void RenderBody(StringBuilder sb, T t, CSharpClassOption options);
		public virtual void RenderConstructor(StringBuilder sb, T t, CSharpClassOption options) {
			string className = GetClassName(t, options);
			foreach (string constructor in options.Constructors) {
				sb.Tab(options.TabLevel).Public().Append(className).AppendLine(constructor);
			}
		}

		public IEnumerable<object> Build(StringBuilder sb, T t, CSharpClassOption option) {
			HashSet<string> imports = new HashSet<string>();
			if (option.Imports != null) { imports.AddRange(option.Imports); }
			foreach (var ns in imports) {
				sb.Append("using ").Append(ns).Terminate();
			}
			bool hasNamespace = !string.IsNullOrEmpty(option.Namespace);
			if (hasNamespace) {
				sb.Tab(option.TabLevel).Append("namespace ").Append(option.Namespace).OpenScope();
				option.TabLevel++;
			}

			string className = GetClassName(t, option);
			sb.Tab(option.TabLevel).Append(option.AccessModifier).Append(" class ").Append(className);
			if(option.Inheritance?.Count() > 0) { 
				foreach (var item in option.Inheritance) {
					if (item == option.Inheritance.First()) {
						sb.Append(" : ");
					} else {
						sb.Comma().Space();
					}
					sb.Append(item);
				}
			}
			sb.OpenScope();
			option.TabLevel++;

			RenderConstructor(sb, t, option);
			RenderBody(sb, t, option);

			option.TabLevel--;
			sb.Tab(option.TabLevel).CloseScope();

			if (hasNamespace) {
				option.TabLevel--;
				sb.CloseScope();
			}

			return new object[] { this, };
		}

		public void Configure(object data) { }

		public IEnumerable<object> Build(StringBuilder sb, object source, object option) {
			return this.ValidateNBuild(sb, source, option);
		}
	}
}
