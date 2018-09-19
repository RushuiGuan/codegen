using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;
using SimpleInjector.Packaging;
using System;

namespace Albatross.CodeGen.SimpleInjector {

	public class Pack : IPackage {
		public void RegisterServices(Container container) {
			container.Register<ICodeGeneratorFactory, CodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<IRunCodeGenerator, RunCodeGenerator>(Lifestyle.Singleton);
			container.Register<IConfigurableCodeGenFactory, CodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<IGetReflectionOnlyType, GetReflectionOnlyType>(Lifestyle.Singleton);

			container.Register<IBuildSqlType, BuildSqlType>(Lifestyle.Singleton);
			container.Register<IBuildVariable, BuildSqlVariable>(Lifestyle.Singleton);
			container.Register<IBuildParameter, BuildProcedureParameter>(Lifestyle.Singleton);
			container.Register<IStoreVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<IGetVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<ICreateVariableName, CreateSqlVariableName>(Lifestyle.Singleton);

		}
	}
}
