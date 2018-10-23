using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class WriteAccessModifier: IWriteObject<AccessModifier> {

		public string Write(AccessModifier accessModifier) {
			switch (accessModifier) {
				case AccessModifier.Public:
				case AccessModifier.Private:
				case AccessModifier.Protected:
				case AccessModifier.Internal:
					return Convert.ToString(accessModifier).ToLower();
				case AccessModifier.ProtectedInternal:
					return "protected internal";
				case AccessModifier.PrivateInternal:
					return "private internal";
			}
			return null;
		}
	}
}
