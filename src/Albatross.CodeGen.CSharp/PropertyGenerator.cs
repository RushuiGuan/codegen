using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class PropertyGenerator : CodeGeneratorBase<Property, object> {
		IWriteObject<AccessModifier> renderAccessModifier;
		IWriteObject<DotNetType> renderType;

		public PropertyGenerator(IWriteObject<AccessModifier> renderAccessModifier, IWriteObject<DotNetType> renderType) {
			this.renderAccessModifier = renderAccessModifier;
			this.renderType = renderType;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Property source, object option) {
			sb.Generate(renderAccessModifier, source.Modifier).Space();
			if (source.Static) { sb.Static(); }

			sb.Generate(renderType, source.Type).Space()
				.Append(source.Name).OpenScope()
				.Tab().Append("get; ");

			if (source.SetModifier != source.Modifier) {
				sb.Generate(renderAccessModifier, source.SetModifier).Space();
			}
			sb.Append("set;").AppendLine().CloseScope();
			return new object[] { this };
		}
	}
}
