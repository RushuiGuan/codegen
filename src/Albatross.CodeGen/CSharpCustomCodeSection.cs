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

		public override void Write(string name, int tabCount, StringBuilder sb) {
			sb.Tab(tabCount).AppendLine($"#region <albatross.codegen.csharp name=\"{name}\">");
			string data = Read(name);
			if (string.IsNullOrEmpty(data)) {
				sb.Tab(tabCount).AppendLine("// Write your custom csharp code here");
			} else {
				sb.AppendLine(data);
			}
			sb.Tab(tabCount).AppendLine($"#endregion </albatross.codegen.csharp>");
		}
	}
}
