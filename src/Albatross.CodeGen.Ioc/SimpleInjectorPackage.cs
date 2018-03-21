using Albatross.CodeGen.CSharp;
using SimpleInjector;
using SimpleInjector.Packaging;
using System;

namespace Albatross.CodeGen.Ioc{

	public class SimpleInjectorPackage : IPackage {
		public void RegisterServices(Container container) {
			container.Register<IFactory<SourceType>, SourceTypeFactory>(Lifestyle.Singleton);
			container.Register<IFactory<OptionType>, OptionTypeFactory>(Lifestyle.Singleton);
			container.Register<ICodeGeneratorFactory, CodeGeneratorFactory>(Lifestyle.Singleton);

			container.Register<IRunCodeGenerator, RunCodeGenerator>(Lifestyle.Singleton);
			container.Register<IConfigurableCodeGenFactory, CodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<IGetReflectionOnlyType, GetReflectionOnlyType>(Lifestyle.Singleton);
		}
	}
}
