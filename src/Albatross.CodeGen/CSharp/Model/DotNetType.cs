﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CSharp.Model {
	public class DotNetType {
        const string VoidType = "System.Void";
		public string Name { get; set; }
		public bool IsGeneric { get; set; }
		public bool IsArray { get; set; }
        public bool IsVoid => Name == VoidType;
        public IEnumerable<DotNetType> GenericTypeArguments { get; set; } = new DotNetType[0];

		private DotNetType() { }
		public DotNetType(string name) : this(name, false, false, null) { }
		public DotNetType(string name, bool isArray, bool isGeneric, IEnumerable<DotNetType> genericTypeArgs) {
			this.Name = name;
			this.IsGeneric = isGeneric;
			this.IsArray = isArray;
			this.GenericTypeArguments = genericTypeArgs ?? new DotNetType[0];
		}
        public DotNetType(Type type) {
            IsArray = type.IsArray;
            if (IsArray) {
                type = type.GetElementType();
            }

            IsGeneric = type.IsGenericType;
            if (IsGeneric) {
                Name = ReflectionExtension.GetGenericTypeName(type.GetGenericTypeDefinition().FullName);
                GenericTypeArguments = (from item in type.GetGenericArguments() select new DotNetType(item)).ToArray();
            } else {
                Name = type.FullName;
            }
        }

        public bool IsAsync  => this.Name == typeof(Task).FullName;
        public override string ToString() {
            StringWriter writer = new StringWriter();
            new Writer.WriteDotNetType().Run(writer, this);
            return writer.ToString();
        }
        public override bool Equals(object obj) {
            return (obj as DotNetType)?.Name == Name;
        }
        public override int GetHashCode() {
            return Name?.GetHashCode()?? 0;
        }
        public static DotNetType Void() => new DotNetType("System.Void");

		public static DotNetType String() => new DotNetType(typeof(string));
		public static DotNetType Char() => new DotNetType(typeof(char));

		public static DotNetType Short() => new DotNetType(typeof(short));
		public static DotNetType Integer() => new DotNetType(typeof(int));
		public static DotNetType Long() => new DotNetType(typeof(long));
		public static DotNetType Decimal() => new DotNetType(typeof(decimal));
        public static DotNetType Single() => new DotNetType(typeof(Single));
		public static DotNetType Double() => new DotNetType(typeof(double));

		public static DotNetType Object() => new DotNetType(typeof(object));

		public static DotNetType DateTime() => new DotNetType(typeof(DateTime));
		public static DotNetType DateTimeOffset() => new DotNetType(typeof(DateTimeOffset));
		public static DotNetType TimeSpan() => new DotNetType(typeof(TimeSpan));

		public static DotNetType Boolean() => new DotNetType(typeof(bool));
		public static DotNetType Byte() => new DotNetType(typeof(byte));
		public static DotNetType ByteArray() => new DotNetType(typeof(byte[]));
		public static DotNetType Guid() => new DotNetType(typeof(Guid));

		public static DotNetType IDbConnection() => new DotNetType("System.Data.IDbConnection");

		public static DotNetType MakeNullable(DotNetType dotNetType) {
			return new DotNetType("System.Nullable", false, true, new DotNetType[] { dotNetType });
		}
		public static DotNetType MakeIEnumerable(DotNetType dotNetType) {
			return new DotNetType("System.Collections.Generic.IEnumerable", false, true, new DotNetType[] { dotNetType });
		}
		public static DotNetType MakeAsync(DotNetType dotNetType) {
            if (dotNetType.IsVoid) {
                return new DotNetType(typeof(Task).FullName);
            } else {
                return new DotNetType(typeof(Task).FullName, false, true, new DotNetType[] { dotNetType });
            }
        }
	}
}