using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest.Mocking {
	public class TestSource { }
	public class TestOption { }


	public class CodeGenFactoryMocking : IMocking {
		public CodeGenFactoryMocking(Mock<ICodeGeneratorFactory> mock) {
			this.mock = mock;
		}
		Mock<ICodeGeneratorFactory> mock;

		public void Setup() {
			CodeGenerator gen = new CodeGenerator {
			};
		}
	}
}
