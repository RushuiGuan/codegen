using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Core {
	/// <summary>
	/// used to register source types in an assembly
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class OptionTypeAttribute : Attribute {
		public string Description { get; set; }
		public OptionTypeAttribute(string description) {
			Description = description;
		}
	}
}
