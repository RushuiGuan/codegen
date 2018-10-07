using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class WriteClass : IWriteObject<Class> {
		IWriteObject<AccessModifier> writeAccessModifier;
		IWriteObject<Constructor> writeConstructor;
		IWriteObject<Method> writeMethod;
		IWriteObject<Field> writeField;
		IWriteObject<Property> writeProperty;

		public WriteClass(IWriteObject<AccessModifier> writeAccessModifier, IWriteObject<Constructor> writeConstructor, IWriteObject<Method> writeMethod, IWriteObject<Field> writeField, IWriteObject<Property> writeProperty) {
			this.writeAccessModifier = writeAccessModifier;
			this.writeConstructor = writeConstructor;
			this.writeMethod = writeMethod;
			this.writeField = writeField;
			this.writeProperty = writeProperty;
		}
		public string Write(Class @class) {
			StringBuilder sb = new StringBuilder();
			if (@class.Imports?.Count() > 0) {
				@class.Imports.Distinct().OrderBy(args => args);
				foreach(var item in @class.Imports) {
					sb.Append("import ").AppendLine(item);
				}
			}

			using (var namespaceWriter = new WriteCSharpScopedObject(sb).BeginScope($"namespace {@class.Namespace}")) {
				using (var classWriter = namespaceWriter.BeginChildScope($"{writeAccessModifier.Write(@class.AccessModifier)} class {@class.Name}")) {

					if(@class.Dependencies?.Count() > 0) {
						Constructor c = new Constructor {
							AccessModifier = AccessModifier.Public,
							Name = @class.Name,
							Static = false,
							Parameters = (from item in @class.Dependencies select new Core.Parameter(item.Name) {
								Type = item.Type,
							}).ToArray(),
						};
						List<Field> fields = new List<Field>();
						foreach (var item in @class.Dependencies) {
							fields.Add(new Field(item.Name) {
								Modifier = AccessModifier.Private,
								Type = item.FieldType ?? item.Type,
							});
							c.Body.This(item.Name).Assignment().Append(item.Name).Semicolon();
							if(item != @class.Dependencies.Last()) {
								c.Body.AppendLine();
							}
						}

						@class.Constructors = new Constructor[] { c }.Union(@class.Constructors ?? new Constructor[0]);
						@class.Fields = fields.Union(@class.Fields ?? new Field[0]);
						
					}
					if(@class.Constructors?.Count() > 0) {
						foreach(var constructor in @class.Constructors) {
							classWriter.Content.AppendLine(writeConstructor.Write(constructor));
						}
					}
					if(@class.Fields?.Count() > 0) {
						foreach(var field in @class.Fields) {
							classWriter.Content.AppendLine(writeField.Write(field));
						}
					}
					if (@class.Properties?.Count() > 0) {
						foreach (var property in @class.Properties) {
							classWriter.Content.AppendLine(writeProperty.Write(property));
						}
					}
					if (@class.Methods?.Count() > 0) {
						foreach (var method in @class.Methods) {
							classWriter.Content.AppendLine(writeMethod.Write(method));
						}
					}
				}
			}
			return sb.ToString();
		}
	}
}
