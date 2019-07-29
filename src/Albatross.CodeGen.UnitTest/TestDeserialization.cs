using Albatross.CodeGen.CSharp.Model;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestDeserialization {

		public string Serialize<T>(T t) {
			StringBuilder sb = new StringBuilder();
			using (var writer = new JsonTextWriter(new StringWriter(sb))) {
				Type type = typeof(T);
				var serializer = new JsonSerializer();
				serializer.Serialize(writer, t);
			}
			return sb.ToString();
		}

		public T Deserialize<T>(string json) {
			using (var reader = new JsonTextReader(new StringReader(json))) {
				Type type = typeof(T);
				var serializer = new JsonSerializer();
				var obj = serializer.Deserialize(reader, type);
				return (T)obj;
			}
		}

		[Test]
		public void TestDotNetType() {
			DotNetType value = new DotNetType(typeof(TestDeserialization));
			string json = Serialize(value);
			DotNetType result = Deserialize<DotNetType>(json);
			Assert.AreEqual(value, result);
		}

		[TestCase(@"c:\git\temp\test.json")]
		public void TestFile(string name) {
			using (var reader = new StreamReader(name)) {
				string content = reader.ReadToEnd();
				Class[] result = Deserialize<Class[]>(content);
				Assert.NotNull(result);
				Assert.AreEqual(1, result.Length);
			}
		}
		[Test]
		public void TestGenericArgument() {
			DotNetType value = new DotNetType(typeof(IEnumerable<string>));
			string json = Serialize(value);
			DotNetType result = Deserialize<DotNetType>(json);
			
			Assert.True(value.Equals(result));
		}

		static IEnumerable<TestCaseData> GetSerializationTestCases() {
			return new TestCaseData[] {
				new TestCaseData(new DotNetType(typeof(string))) {
					ExpectedResult="{\"Name\":\"System.String\",\"IsGeneric\":false,\"IsArray\":false,\"GenericTypeArguments\":[],\"IsAsync\":false,\"IsVoid\":false}"
				},
				new TestCaseData(new DotNetType(typeof(IEnumerable<string>))) {
					ExpectedResult="{\"Name\":\"System.Collections.Generic.IEnumerable\",\"IsGeneric\":true,\"IsArray\":false,\"GenericTypeArguments\":[{\"Name\":\"System.String\",\"IsGeneric\":false,\"IsArray\":false,\"GenericTypeArguments\":[],\"IsAsync\":false,\"IsVoid\":false}],\"IsAsync\":false,\"IsVoid\":false}"
				}
			};
		}
		[TestCaseSource(nameof(GetSerializationTestCases))]
		public string TestSerialize(DotNetType dotNetType) {
			return Serialize<DotNetType>(dotNetType);
		}
	}
}
