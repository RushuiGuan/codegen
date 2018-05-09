using System.IO;
using System.Collections.Generic;
using Albatross.CodeGen.Core;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Albatross.CodeGen {
	/// <summary>
	/// C# Custom codes are marked by the following tags
	/// 
	/// #region <albatross.codegen.csharp name="body">
	///	#endregion </albatross.codegen.csharp>
	///	
	///	custom code tags are case sensitive
	/// </summary>
	public class CSharpCustomCodeSection : CustomCodeSection {
		public override string ApplyTo => GeneratorTarget.CSharp;
		public override string StartTagPattern => @"^\s*\#region\s*<\s*albatross\.codegen\.csharp\s+name\s*=\s*""(.+.*)""\s*>";
		public override string EndTagPattern => @"^\s*\#endregion\s*</albatross\.codegen\.csharp>";

		public override void Write(string name, StringBuilder sb) {
			sb.AppendLine($"#region <albatross.codegen.csharp name=\"{name}\">");
			sb.AppendLine("// Write your custom csharp code here");
			sb.AppendLine($"#endregion </albatross.codegen.csharp>");
		}
	}
}
