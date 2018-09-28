using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class ClassGenerator : CodeGeneratorBase<Class, Class> {
		IRenderCSharp<AccessModifier> renderAccessModifier;
		IRenderCSharp<Constructor> renderConstructor;
		IRenderCSharp<Method> renderMethod;

		public ClassGenerator(IRenderCSharp<AccessModifier> renderAccessModifier, IRenderCSharp<Constructor> renderConstructor, IRenderCSharp<Method> renderMethod) {
			this.renderAccessModifier = renderAccessModifier;
			this.renderConstructor = renderConstructor;
			this.renderMethod = renderMethod;
		}
		public override IEnumerable<object> Build(StringBuilder sb, Class source, Class option) {
			if(source.Imports?.Count() > 0) {
				source.Imports.Distinct().OrderBy(args => args);
				foreach(var item in source.Imports) {
					sb.Append("import ").AppendLine(item);
				}
			}

			if (string.IsNullOrEmpty(source.Namespace)) {
				sb.Namespace().OpenScope();
			}
			return new object[] { this };
		}

		StringBuilder RenderClass(StringBuilder sb, Class source) {
			renderAccessModifier.Render(sb, source.AccessModifier).Class().OpenScope();
			if(source.Constructors?.Count() > 0) {
				foreach(var constructor in source.Constructors) {
//					renderConstructor.Render(new StringBuilder(), constructor);
				}
			}

			sb.CloseScope();
			return sb;
		}
	}
}
