using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Moq;
using NUnit.Framework;
using SimpleInjector;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	public class CompositeGeneratorTest {
		protected Container container = new Container();

		[OneTimeSetUp]
		public void Setup() {
		}


		public CompositeGeneratorTest() {
			Mock<ICodeGenerator<string, string>> gen1 = new Mock<ICodeGenerator<string, string>>();
			IEnumerable<object> used = new object[] { };
			gen1.Setup(gen =>
				gen.Build(It.IsAny<StringBuilder>(), It.Is<string>(name => name == "1"), It.IsAny<string>(), null, out used)
			);
		}
	}
}
