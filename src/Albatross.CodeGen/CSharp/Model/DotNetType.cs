using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Model {
	public class DotNetType {
		public string Name { get; set; }
		public bool IsGeneric { get; set; }
		public bool IsArray { get; set; }
		public IEnumerable<DotNetType> GenericTypeArguments { get; set; }

		private DotNetType() { }
		public DotNetType(string name) : this(name, false, false, null) { }
		public DotNetType(string name, bool isArray, bool isGeneric, IEnumerable<DotNetType> genericTypeArgs) {
			this.Name = name;
			this.IsGeneric = isGeneric;
			this.IsArray = isArray;
			this.GenericTypeArguments = genericTypeArgs ?? new DotNetType[0];
		}




		public static DotNetType Void() => new DotNetType("void");

		public static DotNetType String() => new DotNetType("System.String");
		public static DotNetType Char() => new DotNetType("char");

		public static DotNetType Short() => new DotNetType("short");
		public static DotNetType Integer() => new DotNetType("System.Int32");
		public static DotNetType Long() => new DotNetType("long");
		public static DotNetType Decimal() => new DotNetType("decimal");
		public static DotNetType Single() => new DotNetType("single");
		public static DotNetType Double() => new DotNetType("System.Double");

		public static DotNetType Object() => new DotNetType("object");

		public static DotNetType DateTime() => new DotNetType("DateTime");
		public static DotNetType DateTimeOffset() => new DotNetType("DateTimeOffset");
		public static DotNetType TimeSpan() => new DotNetType("TimeSpan");

		public static DotNetType Boolean() => new DotNetType("bool");
		public static DotNetType Byte() => new DotNetType("byte");
		public static DotNetType ByteArray() => new DotNetType("byte[]");
		public static DotNetType Guid() => new DotNetType("Guid");

		public static DotNetType IDbConnection() => new DotNetType("System.Data.IDbConnection");

		public static DotNetType MakeNullable(DotNetType dotNetType) {
			return new DotNetType("System.Nullable", false, true, new DotNetType[] { dotNetType });
		}
		public static DotNetType MakeIEnumerable(DotNetType dotNetType) {
			return new DotNetType("System.Collections.Generic.IEnumerable", false, true, new DotNetType[] { dotNetType });
		}
	}
}