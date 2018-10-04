using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class DotNetType {
		public DotNetType(string name) {
			Name = name;
		}
		public string Name { get; set; }
		public bool IsGeneric { get; set; }
		public IEnumerable<DotNetType> GenericTypes { get; set; }

		public static readonly DotNetType String = new DotNetType("string");
		public static readonly DotNetType Char = new DotNetType("char");

		public static readonly DotNetType Short = new DotNetType("short");
		public static readonly DotNetType Integer = new DotNetType("int");
		public static readonly DotNetType Long = new DotNetType("long");
		public static readonly DotNetType Decimal = new DotNetType("decimal");
		public static readonly DotNetType Single = new DotNetType("single");
		public static readonly DotNetType Double = new DotNetType("double");

		public static readonly DotNetType Object = new DotNetType("object");

		public static readonly DotNetType DateTime = new DotNetType("DateTime");
		public static readonly DotNetType DateTimeOffset = new DotNetType("DateTimeOffset");
		public static readonly DotNetType TimeSpan= new DotNetType("TimeSpan");

		public static readonly DotNetType Boolean = new DotNetType("bool");
		public static readonly DotNetType Byte = new DotNetType("byte");
		public static readonly DotNetType ByteArray = new DotNetType("byte[]");
		public static readonly DotNetType Guid = new DotNetType("Guid");

		
	}
}
