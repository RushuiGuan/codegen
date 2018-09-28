using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class PropertyGenerator : CodeGeneratorBase<Property, object> {
		IRenderCSharp<AccessModifier> renderAccessModifier;
		IRenderCSharp<DotNetType> renderType;

		public PropertyGenerator(IRenderCSharp<AccessModifier> renderAccessModifier, IRenderCSharp<DotNetType> renderType) {
			this.renderAccessModifier = renderAccessModifier;
			this.renderType = renderType;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Property source, object option) {
			renderAccessModifier
				.Render(sb, source.Modifier).Space();
			if (source.Static) { sb.Static(); }

			renderType.Render(sb, source.Type).Space()
				.Append(source.Name).OpenScope()
				.Tab().Append("get; ");

			if (source.SetModifier != source.Modifier) {
				renderAccessModifier.Render(sb, source.SetModifier).Space();
			}
			sb.Append("set;").AppendLine().CloseScope();
			return new object[] { this };
		}
	}
}
