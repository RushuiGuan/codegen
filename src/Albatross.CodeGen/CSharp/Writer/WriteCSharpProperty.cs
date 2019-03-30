using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteCSharpProperty : CodeGeneratorBase<Property> {
		ICodeGenerator<AccessModifier> writeAccessModifier;
		ICodeGenerator<DotNetType> writeType;

		public WriteCSharpProperty(ICodeGenerator<AccessModifier> renderAccessModifier, ICodeGenerator<DotNetType> renderType) {
			this.writeAccessModifier = renderAccessModifier;
			this.writeType = renderType;
		}

		public override void Run(TextWriter writer, Property property) {
            writer.Run(writeAccessModifier, property.Modifier).Space();
			if (property.Static) { writer.Static(); }

            writer.Run(writeType, property.Type).Space().Append(property.Name);

            using (var scope = writer.BeginScope()) {
                scope.Writer.Append(" get; ");
				if (property.SetModifier != property.Modifier) {
                    scope.Writer.Run(writeAccessModifier, property.SetModifier).Space();
				}
				scope.Writer.Append("set");
			}
		}
	}
}
