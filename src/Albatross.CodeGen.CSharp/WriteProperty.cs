using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class WriteProperty : IWriteObject<Property> {
		IWriteObject<AccessModifier> writeAccessModifier;
		IWriteObject<DotNetType> writeType;

		public WriteProperty(IWriteObject<AccessModifier> renderAccessModifier, IWriteObject<DotNetType> renderType) {
			this.writeAccessModifier = renderAccessModifier;
			this.writeType = renderType;
		}

		public string Write(Property property) {
			StringBuilder sb = new StringBuilder();
			sb.Write(writeAccessModifier, property.Modifier).Space();
			if (property.Static) { sb.Static(); }
			sb.Write(writeType, property.Type).Space().Append(property.Name);

			using (var scope = new WriteSingleLineCSharpScopedObject(sb).BeginScope()) {
				scope.Content.Append(" get; ");
				if (property.SetModifier != property.Modifier) {
					scope.Content.Write(writeAccessModifier, property.SetModifier).Space();
				}
				scope.Content.Append("set; ");
			}
			return sb.ToString();
		}
	}
}
