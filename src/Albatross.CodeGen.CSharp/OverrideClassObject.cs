using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class OverrideClassObject : IOverrideClassObject {
		IGetMethodSignature getMethodSignature;

		public OverrideClassObject(IGetMethodSignature getMethodSignature) {
			this.getMethodSignature = getMethodSignature;
		}

		public Class Get(Class src, Class @override) {
			src.Imports = (src.Imports ?? new string[0]).Union(@override.Imports ?? new string[0]).Distinct().OrderBy(args => args);
			if (!string.IsNullOrEmpty(@override.Namespace)) { src.Namespace = @override.Namespace; }
			if (!string.IsNullOrEmpty(@override.Name)) { src.Name = @override.Name; }

			Dictionary<string, Property> properties = new Dictionary<string, Property>();
			if (src.Properties?.Count() > 0) {
				foreach (var item in src.Properties) { properties.Add(item.Name, item); }
			}
			if (@override.Properties?.Count() > 0) {
				foreach (var item in @override.Properties) { properties[item.Name] = item; }
			}
			src.Properties = properties.Values;

			Dictionary<string, Field> fields = new Dictionary<string, Field>();
			if (src.Fields?.Count() > 0) {
				foreach (var item in src.Fields) { fields.Add(item.Name, item); }
			}
			if (@override.Fields?.Count() > 0) {
				foreach (var item in @override.Fields) { fields[item.Name] = item; }
			}
			src.Fields = fields.Values;

			Dictionary<string, Method> methods = new Dictionary<string, Method>();
			if (src.Methods?.Count() > 0) {
				foreach (var item in src.Methods) { methods.Add(getMethodSignature.Get(item), item); }
			}
			if (@override.Methods?.Count() > 0) {
				foreach (var item in @override.Methods) { methods[getMethodSignature.Get(item)] = item; }
			}
			src.Methods = methods.Values;

			Dictionary<string, Constructor> constructors = new Dictionary<string, Constructor>();
			if (src.Constructors?.Count() > 0) {
				foreach (var item in src.Constructors) { constructors.Add(getMethodSignature.Get(item), item); }
			}
			if (@override.Constructors?.Count() > 0) {
				foreach (var item in @override.Constructors) { constructors[getMethodSignature.Get(item)] = item; }
			}
			src.Constructors = constructors.Values;

			return src;
		}
	}
}