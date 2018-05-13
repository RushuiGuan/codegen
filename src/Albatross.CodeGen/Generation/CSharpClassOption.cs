using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;

namespace Albatross.CodeGen.Generation {
	/// <summary>
	/// Options class for CSharp class generation
	/// </summary>
	[OptionType("C# Class Option")]
	public class CSharpClassOption {
		public string Name { get; set; }
		public string Prefix { get; set; }
		public string Postfix { get; set; }
		public int TabLevel { get; set; }

		public string Namespace { get; set; }
		public string AccessModifier { get; set; } = "public";
		public IEnumerable<string> Inheritance { get; set; }
		public IEnumerable<string> Imports { get; set; } = new string[] { "System" };
		public IEnumerable<string> Constructors { get; set; } = new string[0];
		public bool PartialClass { get; set; }
		public Type DefaultIdentityColumnType { get; set; } = typeof(int);
		/// <summary>
		/// Force a type on a property.  Useful when Enum types are used on a property.
		/// </summary>
		public Dictionary<string, string> PropertyTypeOverrides { get; set; } = new Dictionary<string, string>();
	}
}
