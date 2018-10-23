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

			src.Dependencies = src.Dependencies.Merge(@override.Dependencies, args=>args.Name);
			src.Properties = src.Properties.Merge(@override.Properties, args => args.Name);
			src.Fields = src.Fields.Merge(@override.Fields, args => args.Name);
			src.Methods = src.Methods.Merge(@override.Methods, args => getMethodSignature.Get(args));
			src.Constructors = src.Constructors.Merge(@override.Constructors, args => args.Name);

			return src;
		}
	}
}