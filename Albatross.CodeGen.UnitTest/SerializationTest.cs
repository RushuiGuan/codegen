using Albatross.CodeGen.Database;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class SerializationTest {

		[Test]
		public void StepSerialization() {
			Step step = new Step {
				 Generator = "test",
				  Source = new Table { Schema = "schema", Name = "table", Server = new Server { DatabaseType = "sql", DataSource = "prod", InitialCatalog ="albatross", } },
				  SourceType = typeof(Table),
			};
			string text = JsonConvert.SerializeObject(step);
			Step newValue = JsonConvert.DeserializeObject<Step>(text);

			//Assert.AreEqual(step.Source, newValue.Source);
			Assert.AreEqual(step.SourceType, newValue.SourceType);
			Assert.AreEqual(step.Generator, newValue.Generator);
		}
	}
}
