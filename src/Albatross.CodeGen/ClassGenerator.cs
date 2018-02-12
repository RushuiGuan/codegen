using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	/// <summary>
	/// ClassGenerator is an abstract class for C# class code generation.  It uses a fixed option type <see cref="Albatross.CodeGen.ClassOption" /> but the Source Type is left open.  It will take care of 
	/// namespace, class scope, class name and constructor generation using the information provided by the <see cref="Albatross.CodeGen.ClassOption" /> object.  It leaves the rest of the body creation to a abstract 
	/// method <see cref="Albatross.CodeGen.ClassGenerator{T}.RenderBody(StringBuilder, int, T, ClassOption)"/>.
	/// </summary>
	/// <typeparam name="T">the source type</typeparam>
	public abstract class ClassGenerator<T> : ICodeGenerator<T, ClassOption> where T : class {
		public event Func<StringBuilder, IEnumerable<object>> Yield;
		/// <summary>
		/// Return the name of the class.  By default, it will use the <see cref="Albatross.CodeGen.ClassOption.Name"/> property.  Override this method to change the behavior.
		/// </summary>
		/// <param name="t">Source input</param>
		/// <param name="option">Option input</param>
		/// <returns>The name for this class</returns>
		public virtual string GetClassName(T t, ClassOption option) {
			return option.Name;
		}
		public abstract void RenderBody(StringBuilder sb, int tabLevel, T t, ClassOption options);

		public IEnumerable<object>  Build(StringBuilder sb, T t, ClassOption option) {
			if (option.Imports != null) {
				foreach (var ns in option.Imports) {
					sb.Append("using ").Append(ns).Terminate();
				}
			}
			sb.AppendLine();

			string className = GetClassName(t, option);
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
