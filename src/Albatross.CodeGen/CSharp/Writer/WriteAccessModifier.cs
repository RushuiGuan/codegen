using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer {
	public class WriteAccessModifier: CodeGeneratorBase<AccessModifier> {
		public override void Run(TextWriter writer, AccessModifier accessModifier) {
			switch (accessModifier) {
				case AccessModifier.Public:
				case AccessModifier.Private:
				case AccessModifier.Protected:
				case AccessModifier.Internal:
					writer.Append(Convert.ToString(accessModifier).ToLower());
					break;
				case AccessModifier.ProtectedInternal:
					writer.Append("protected internal");
					break;
				case AccessModifier.PrivateInternal:
					writer.Append("private internal"); 
					break;
			}
		}
	}
}
