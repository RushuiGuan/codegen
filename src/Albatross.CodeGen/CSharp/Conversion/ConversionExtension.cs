using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Conversion {
	public static class ConversionExtension {
		public static AccessModifier GetAccessModifier(this MethodInfo method) {
			AccessModifier accessModifier = AccessModifier.Unknown;

			if (method.IsPublic) {
				accessModifier = accessModifier | AccessModifier.Public;
			}
			if (method.IsPrivate) {
				accessModifier = accessModifier | AccessModifier.Private;
			}
			if (method.IsAssembly) {
				accessModifier = accessModifier | AccessModifier.Internal;
			}
			if (method.IsFamily) {
				accessModifier = accessModifier | AccessModifier.Protected;
			}
			if (accessModifier == AccessModifier.Unknown) {
				throw new ConversionException("Unknown access modifier");
			}
			return accessModifier;
		}
	}
}