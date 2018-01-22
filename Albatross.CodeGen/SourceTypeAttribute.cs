using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	/// <summary>
	/// used to register source types in an assembly
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class SourceTypeAttribute : Attribute {
		public string Description { get; set; }
		public SourceTypeAttribute(string description) {
			Description = description;
		}
	}
}
