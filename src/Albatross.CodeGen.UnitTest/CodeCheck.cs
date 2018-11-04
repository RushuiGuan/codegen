using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class CodeCheck {

		[TestCase(@"c:\temp\test.txt", ExpectedResult =@"c:\temp")]
		[TestCase(@"c:\temp\test*.txt", ExpectedResult = @"c:\temp")]
		public string PathCheck(string path) {
			return Path.GetDirectoryName(path);
		}


		[TestCase(@"c:\temp\test.txt", ExpectedResult = @"test.txt")]
		[TestCase(@"c:\temp\test*.txt", ExpectedResult = @"test*.txt")]
		public string FileCheck(string path) {
			return Path.GetFileName(path);
		}

		[Test]
		public void GenericType() {
			Type type = typeof(ICodeGenerator<,>);
			type.ToString();
		}

		[Test]
		public void TestAssignable() {
			Type string_type = typeof(string);
			Type object_type = typeof(object);
			//Assert.True(string_type.IsAssignableFrom(object_type));
			Assert.True(object_type.IsAssignableFrom(string_type));
		}

		[Test]
		public void xx() {
			ICodeGenerator<object, object> a = null;
			ICodeGenerator<string, string> b = null;
			ICodeGenerator c = null;
			b = a;
			c = b;
		}

		[Test]
		public void UnionTest() {
			string[] x = new string[] { "1" };
			x.Union(null);
		}

		public class T1Attribute: Attribute { }
		public class T2Attribute : T1Attribute { }

		[T2]
		[Test]
		public void AttributeTest() {
			Type type = this.GetType();
			MethodInfo info = type.GetMethod(nameof(AttributeTest));
			var attrib = info.GetCustomAttribute<T1Attribute>();
			Assert.NotNull(attrib);
		}

	}
}
