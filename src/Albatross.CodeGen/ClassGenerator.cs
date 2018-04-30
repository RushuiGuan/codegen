using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen {
	/// <summary>
	/// ClassGenerator is an abstract class for C# class code generation.  It uses a fixed option type <see cref="Albatross.CodeGen.CSharp.ClassOption" /> but the Source Type is left open.  It will take care of 
	/// namespace, class scope, class name and constructor generation using the information provided by the <see cref="Albatross.CodeGen.CSharp.ClassOption" /> object.  It leaves the rest of the body creation to a abstract 
	/// method <see cref="Albatross.CodeGen.CSharp.ClassGenerator{T}.RenderBody(StringBuilder, int, T, ClassOption)"/>.
	/// </summary>
	/// <typeparam name="T">the source type</typeparam>
	public abstract class ClassGenerator<T> : ICodeGenerator<T, ClassOption> where T : class {

#pragma warning disable 0067
		public event Func<StringBuilder, IEnumerable<object>> Yield;
#pragma warning restore 0067

		/// <summary>
		/// Return the name of the class.  By default, it will use the <see cref="Albatross.CodeGen.CSharp.ClassOption.Name"/> property.  Override this method to change the behavior.
		/// </summary>
		/// <param name="t">Source input</param>
		/// <param name="option">Option input</param>
		/// <returns>The name for this class</returns>
		public virtual string GetClassName(T t, ClassOption option) {
			return option.Name;
		}
		public abstract void RenderBody(StringBuilder sb, int tabLevel, T t, ClassOption options);
		public virtual void RenderConstructor(StringBuilder sb, int tabLevel, T t, ClassOption options) {
			string className = GetClassName(t, options);
			foreach (string constructor in options.Constructors) {
				sb.Tab(tabLevel).Public().Append(className).Append(constructor).EmptyScope();
			}
		}

		public IEnumerable<object> Build(StringBuilder sb, T t, ClassOption option) {
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
			sb.Tab(level).Append(option.AccessModifier).Append(" class ").Append(className);
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
			level++;

			RenderConstructor(sb, level, t, option);
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
