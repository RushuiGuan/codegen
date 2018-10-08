using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;
using SimpleInjector.Packaging;
using System;

namespace Albatross.CodeGen.SimpleInjector {

	public class Pack : IPackage {
		public void RegisterServices(Container container) {
			new Albatross.Database.SqlServer.SimpleInjector.Pack().RegisterServices(container);

			container.Register<ICodeGeneratorFactory, CodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<IRunCodeGenerator, RunCodeGenerator>(Lifestyle.Singleton);
			container.Register<IConfigurableCodeGenFactory, CodeGeneratorFactory>(Lifestyle.Singleton);

			container.Register<IBuildSqlType, BuildSqlType>(Lifestyle.Singleton);
			container.Register<IBuildVariable, BuildSqlVariable>(Lifestyle.Singleton);
			container.Register<IBuildParameter, BuildProcedureParameter>(Lifestyle.Singleton);
			container.Register<IStoreVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<IGetVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<ICreateVariableName, CreateSqlVariableName>(Lifestyle.Singleton);

			container.Register<IGetDotNetType, GetDotNetType>(Lifestyle.Singleton);
			container.Register<IOverrideClassObject, OverrideClassObject>(Lifestyle.Singleton);
			container.Register<IGetMethodSignature, GetMethodSignature>(Lifestyle.Singleton);

			container.Register<IWriteObject<Property>, WriteProperty>(Lifestyle.Singleton);
			container.Register<IWriteObject<DotNetType>, WriteDotNetType>(Lifestyle.Singleton);
			container.Register<IWriteObject<AccessModifier>, WriteAccessModifier>(Lifestyle.Singleton);
			container.Register<IWriteObject<Class>, WriteClass>(Lifestyle.Singleton);
			container.Register<IWriteObject<Constructor>, WriteConstructor>(Lifestyle.Singleton);
			container.Register<IWriteObject<Parameter>, WriteParameter>(Lifestyle.Singleton);
			container.Register<IWriteObject<Method>, WriteMethod>(Lifestyle.Singleton);
			container.Register<IWriteObject<Field>, WriteField>(Lifestyle.Singleton);

			container.RegisterInstance<IObjectFactory>(new ObjectFactory(container));
		}

		public class ObjectFactory : IObjectFactory {
			Container container;
			public ObjectFactory(Container container) {
				this.container = container;
			}

			public T Create<T>() where T : class {
				return container.GetInstance<T>();
			}

			public object Create(Type type) {
				return container.GetInstance(type);
			}
		}
	}
}
