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
	public class ReadCSharpCustomCode : ReadCustomCode {
		public override string StartTagPattern => @"^\s*\#region\s*<\s*albatross\.codegen\.csharp\s+name\s*=\s*""([a-zA-Z_][a-zA-Z0-9_]*)""\s*>";
		public override string EndTagPattern => @"^\s*\#endregion\s*</albatross\.codegen\.csharp>";
	}
}
