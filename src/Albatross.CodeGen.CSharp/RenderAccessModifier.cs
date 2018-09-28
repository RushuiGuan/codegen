using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class RenderAccessModifier: IRenderCSharp<AccessModifier> {

		public StringBuilder Render(StringBuilder sb, AccessModifier accessModifier) {
			switch (accessModifier) {
				case AccessModifier.Public:
				case AccessModifier.Private:
				case AccessModifier.Protected:
				case AccessModifier.Internal:
					sb.Append(Convert.ToString(accessModifier).ToLower());
					break;
				case AccessModifier.ProtectedInternal:
					sb.Append("protected internal");
					break;
				case AccessModifier.PrivateInternal:
					sb.Append("private internal");
					break;
			}
			return sb;
		}
	}
}
