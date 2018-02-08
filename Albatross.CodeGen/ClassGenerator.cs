using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	/// <summary>
	/// an abstract class created to speed up C# class code generation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ClassGenerator<T> : ICodeGenerator<T, ClassOption> where T : class {
		public event Func<StringBuilder, IEnumerable<object>> Yield;
		public abstract string GetClassName(T t);
		public abstract void RenderBody(StringBuilder sb, int tabLevel, T t, ClassOption options);

		public IEnumerable<object>  Build(StringBuilder sb, T t, ClassOption option) {
			if (option.Imports != null) {
				foreach (var ns in option.Imports) {
					sb.Append("using ").Append(ns).Terminate();
				}
			}
			sb.AppendLine();

			string className = GetClassName(t);
			int level = 0;
			sb.Tab(level).Append("namespace ").Append(option.Namespace).OpenScope();
			level++;
			sb.Tab(level).Append(option.AccessModifier).Append(" class ").Append(className).Append(" : ").Append(option.BaseClass).OpenScope();
			level++;

			foreach (string constructor in option.Constructors) {
				sb.Tab(level).Public().Append(className).Append(constructor).EmptyScope();
			}

			RenderBody(sb, level, t, option);

			level--;
			sb.Tab(level).CloseScope(false);
			level--;
			sb.CloseScope(terminate: false);

			return new object[] { this, };
		}

		public void Configure(object data) { }
	}
}
