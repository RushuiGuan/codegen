using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	/// <summary>
	/// an abstract class created to speed up C# class code generation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ClassGenerator<T> : ICodeGenerator<T> where T : class {
		public abstract string Category { get; }
		public abstract string Description { get; }
		public abstract string Name { get; }
		public Type SourceType => typeof(T);
		public string Target => "c#";
		public ClassOptions Options { get; set; }


		public abstract string GetClassName(T t);
		public abstract void RenderBody(StringBuilder sb, int tabLevel, T t, ClassOptions options, ICodeGeneratorFactory factory);


		public StringBuilder Build(StringBuilder sb, T t, object options, ICodeGeneratorFactory factory) {
			ClassOptions opt = options as ClassOptions;
			if (opt == null) { opt = new ClassOptions(); }
			if (opt.Imports != null) {
				foreach (var ns in opt.Imports) {
					sb.Append("using ").Append(ns).Terminate();
				}
			}
			sb.AppendLine();

			string className = GetClassName(t);
			int level = 0;
			sb.Tab(level).Append("namespace ").Append(opt.Namespace).OpenScope();
			level++;
			sb.Tab(level).Append(opt.AccessModifier).Append(" class ").Append(className).Append(" : ").Append(opt.BaseClass).OpenScope();
			level++;

			foreach (string constructor in opt.Constructors) {
				sb.Tab(level).Public().Append(className).Append(constructor).EmptyScope();
			}

			RenderBody(sb, level, t, opt, factory);

			level--;
			sb.Tab(level).CloseScope(false);
			level--;
			sb.CloseScope(terminate: false);
			return sb;
		}



		StringBuilder ICodeGenerator.Build(StringBuilder sb, object t, object options, ICodeGeneratorFactory factory) {
			return this.Build(sb, (T)t, options, factory);
		}
	}
}
