using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	/// <summary>
	/// an abstract class created to speed up C# class code generation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ClassGenerator<T> : ICodeGenerator<T, ClassOptions> where T : class {
		public abstract string Category { get; }
		public abstract string Description { get; }
		public abstract string Name { get; }
		public Type SourceType => typeof(T);
		public Type OptionType => typeof(ClassOptions);
		public string Target => "c#";

		public IEnumerable<ICodeGenerator<T, ClassOptions>> Children { get; set; }
		public abstract string GetClassName(T t);
		public abstract void RenderBody(StringBuilder sb, int tabLevel, T t, ClassOptions options, ICodeGeneratorFactory factory);


		public StringBuilder Build(StringBuilder sb, T t, ClassOptions option, ICodeGeneratorFactory factory) {
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

			RenderBody(sb, level, t, option, factory);

			level--;
			sb.Tab(level).CloseScope(false);
			level--;
			sb.CloseScope(terminate: false);
			return sb;
		}



		StringBuilder ICodeGenerator.Build(StringBuilder sb, object t, object options,  ICodeGeneratorFactory factory) {
			return this.Build(sb, (T)t, (ClassOptions)options, factory);
		}
	}
}
