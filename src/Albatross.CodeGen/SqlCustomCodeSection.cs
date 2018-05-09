using System.IO;
using System.Collections.Generic;
using Albatross.CodeGen.Core;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Albatross.CodeGen {
	/// <summary>
	/// sql Custom codes are marked by the following tags
	/// 
	/// #region <albatross.codegen.sql name="body">
	///	#endregion </albatross.codegen.sql>
	///	
	///	custom code tags are case sensitive
	/// </summary>
	public class SqlCustomCodeSection : CustomCodeSection {
		public override string ApplyTo => GeneratorTarget.Sql;
		public override string StartTagPattern => @"$\s*--\s*<albatross.codegen.sql name=""([a-zA-Z_][a-zA-Z0-9_]*)"">";
		public override string EndTagPattern => @"$\s*--\s*</albatross.codegen.sql>";

		public override void Write(string name, StringBuilder sb) {
			sb.AppendLine($"-- <albatross.codegen.sql name=\"{name}\">");
			sb.AppendLine("-- Write your custom sql code here");
			sb.AppendLine($"-- </albatross.codegen.sql>");
		}
	}
}
