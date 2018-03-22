using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest
{
    public static class Extension
    {

		#region datatypes
		public static SqlType UnicodeString(int length) {
			return new SqlType {
				Name = "nvarchar",
				MaxLength = length,
			};
		}
		public static SqlType NonUnicodeString(int length) {
			return new SqlType {
				Name = "varchar",
				MaxLength = length,
			};
		}

		public static SqlType NonUnicodeFixedString(int length) {
			return new SqlType {
				Name = "char",
				MaxLength = length,
			};
		}

		public static SqlType UnicodeFixedString(int length) {
			return new SqlType {
				Name = "nchar",
				MaxLength = length,
			};
		}
		public static SqlType Int() {
			return new SqlType { Name = "int" };
		}
		public static SqlType BigInt() {
			return new SqlType { Name = "bigint" };
		}
		public static SqlType DateTime() {
			return new SqlType { Name = "datetime" };
		}
		#endregion


		public static Variable UnicodeStringVariable(string name, int length) {
			return new Variable {
				Name= name,
				Type = UnicodeString(length),
			};
		}
		public static Variable NonUnicodeStringVariable(string name, int length) {
			return new Variable {
				Name = name,
				Type = NonUnicodeString(length),
			};
		}
	}
}
