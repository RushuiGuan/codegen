using Albatross.Database;
using Moq;
using System.Collections.Generic;

namespace Albatross.CodeGen.UnitTest.Mocking {
	public abstract class ProcedureMocking: IMocking {
		public abstract string Name { get; }
		public abstract string Schema { get; }
		public abstract IEnumerable<Parameter> Parameters { get; }

		Mock<IGetProcedure> getProcedure;

		public ProcedureMocking(Mock<IGetProcedure> getProcedure) {
			this.getProcedure = getProcedure;
		}


		public void Setup() {
			getProcedure.Setup(args => args.Get(It.IsAny<Albatross.Database.Database>(), It.Is<string>(schema=> string.Equals(schema, Schema, System.StringComparison.InvariantCultureIgnoreCase)), It.Is<string>(name=> string.Equals(name, Name, System.StringComparison.InvariantCultureIgnoreCase)))).Returns(new Procedure {
				Schema = Schema,
				Name= Name,
				Parameters = Parameters,
			});
		}
	}
}
