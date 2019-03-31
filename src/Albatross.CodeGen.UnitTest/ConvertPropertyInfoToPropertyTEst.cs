using Albatross.CodeGen.CSharp.Conversion;
using Albatross.CodeGen.CSharp.Model;
using Autofac;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class ConvertPropertyInfoToPropertyTest :TestBase {

		public class TestClass {
			public string Text { get; set; }
			public string ReadOnlyText { get; }
			public static int Number { get; set; }
		}


		public static IEnumerable<TestCaseData> GetTestData() {
			Type type = typeof(TestClass);

			return new TestCaseData[] {
				new TestCaseData(type.GetProperty(nameof(TestClass.Text)), new Property(nameof(TestClass.Text)){  Type = DotNetType.String, CanWrite = true, CanRead = true, } ),
				new TestCaseData(type.GetProperty(nameof(TestClass.ReadOnlyText)), new Property(nameof(TestClass.ReadOnlyText)){  Type = DotNetType.String, CanWrite = false, CanRead = true, } ),
				new TestCaseData(type.GetProperty(nameof(TestClass.Number)), new Property(nameof(TestClass.Number)){  Type = DotNetType.Integer, CanWrite = true, CanRead = true, Static = true, } ),
			};
		}


		[TestCaseSource(nameof(GetTestData))]
		public void Run(PropertyInfo propertyInfo, Property expected) {
			ConvertPropertyInfoToProperty handle = container.Resolve<ConvertPropertyInfoToProperty>();
			var result = handle.Convert(propertyInfo);
			Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
		}
	}
}
