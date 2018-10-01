using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class ClassGenerator : CodeGeneratorBase<Class, Class> {
		IWriteObject<AccessModifier> writeAccessModifier;
		IWriteObject<Constructor> writeConstructor;
		IWriteObject<Method> writeMethod;
		IWriteObject<Field> writeField;
		IWriteObject<Property> writeProperty;

		public ClassGenerator(IWriteObject<AccessModifier> writeAccessModifier, IWriteObject<Constructor> writeConstructor, IWriteObject<Method> writeMethod, IWriteObject<Field> writeField, IWriteObject<Property> writeProperty) {
			this.writeAccessModifier = writeAccessModifier;
			this.writeConstructor = writeConstructor;
			this.writeMethod = writeMethod;
			this.writeField = writeField;
			this.writeProperty = writeProperty;
		}
		public override IEnumerable<object> Build(StringBuilder sb, Class source, Class option) {
			if(source.Imports?.Count() > 0) {
				source.Imports.Distinct().OrderBy(args => args);
				foreach(var item in source.Imports) {
					sb.Append("import ").AppendLine(item);
				}
			}

			using (var namespaceWriter = new WriteCSharpScopedObject(sb).BeginScope($"namespace {source.Namespace}")) {
				using (var classWriter = namespaceWriter.BeginChildScope($"{writeAccessModifier.Write(source.AccessModifier)} class {source.Name}")) {
					if(source.Constructors?.Count() > 0) {
						foreach(var constructor in source.Constructors) {
							classWriter.Content.AppendLine(writeConstructor.Write(constructor));
						}
					}
					if(source.Fields?.Count() > 0) {
						foreach(var field in source.Fields) {
							classWriter.Content.AppendLine(writeField.Write(field));
						}
					}
					if (source.Properties?.Count() > 0) {
						foreach (var property in source.Properties) {
							classWriter.Content.AppendLine(writeProperty.Write(property));
						}
					}
					if (source.Methods?.Count() > 0) {
						foreach (var method in source.Methods) {
							classWriter.Content.AppendLine(writeMethod.Write(method));
						}
					}
				}
			}
			return new object[] { this };
		}
	}
}
