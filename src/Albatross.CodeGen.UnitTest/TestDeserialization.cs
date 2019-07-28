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
	}
}
