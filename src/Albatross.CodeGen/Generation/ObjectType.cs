using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Generation {
	[SourceType("C# class")]
	public class ObjectType {
		public string ClassName { get; set; }
		public string AssemblyLocation { get; set; }
	}
}
