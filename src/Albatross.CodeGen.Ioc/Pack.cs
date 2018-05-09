using Albatross.CodeGen.Core;
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

			container.Register<IRenderSqlType, RenderSqlType>(Lifestyle.Singleton);
			container.Register<IRenderSqlVariable, RenderSqlVariable>(Lifestyle.Singleton);
			container.Register<IRenderSqlParameter, RenderSqlParameter>(Lifestyle.Singleton);
			container.Register<IStoreSqlVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<IRetrieveSqlVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<ICreateSqlVariableName, CreateSqlVariableName>(Lifestyle.Singleton);
			container.Register<IConvertSqlDataType, ConvertDataType>(Lifestyle.Singleton);
			container.Register<IRenderDotNetType, RenderDotNetType>(Lifestyle.Singleton);
			container.Register<IParseSqlType, ParseSqlType>(Lifestyle.Singleton);

			var items = new Registration[] {
				Lifestyle.Singleton.CreateRegistration<CSharpCustomCodeSection>(container),
				Lifestyle.Singleton.CreateRegistration<SqlCustomCodeSection>(container),
			};
			container.RegisterCollection<ICustomCodeSection>(items);
			container.Register<ICustomCodeSectionStrategy, CustomCodeSectionStrategy>(Lifestyle.Singleton);
		}
	}
}
