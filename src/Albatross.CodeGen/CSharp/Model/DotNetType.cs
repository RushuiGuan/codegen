using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Model {
	public class DotNetType {
		public DotNetType(string name):this(name, false, false, null) {
		}


		public DotNetType(string name, bool isArray, bool isGeneric, IEnumerable<DotNetType> genericTypeArgs) {
			this.Name = name;
			this.IsGeneric = isGeneric;
			this.IsArray = isArray;
			this.GenericTypeArguments = genericTypeArgs ?? new DotNetType[0];
		}

		public string Name { get; private set; }
		public bool IsGeneric { get;private set; }
		public bool IsArray { get; private set; }
		public IEnumerable<DotNetType> GenericTypeArguments { get; private set; }

		public static readonly DotNetType Void = new DotNetType("void");

		public static readonly DotNetType String = new DotNetType("System.String");
		public static readonly DotNetType Char = new DotNetType("char");

		public static readonly DotNetType Short = new DotNetType("short");
		public static readonly DotNetType Integer = new DotNetType("System.Int32");
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

		public static readonly DotNetType IDbConnection = new DotNetType("System.Data.IDbConnection");

		public static DotNetType MakeNullable(DotNetType dotNetType) {
			return new DotNetType("System.Nullable", false, true, new DotNetType[] { dotNetType });
		}
		public static DotNetType MakeIEnumerable(DotNetType dotNetType) {
			return new DotNetType("System.Collections.Generic.IEnumerable", false, true, new DotNetType[] { dotNetType });
		}
	}
}
