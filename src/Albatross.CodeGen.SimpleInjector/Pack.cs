using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Conversion;
using Albatross.CodeGen.CSharp.Writer;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;
using SimpleInjector.Packaging;
using System;
using System.Reflection;

namespace Albatross.CodeGen.SimpleInjector {

	public class Pack : IPackage {
		public void RegisterServices(Container container) {
			new Albatross.Database.SqlServer.SimpleInjector.Pack().RegisterServices(container);


			container.Register<IBuildSqlType, BuildSqlType>(Lifestyle.Singleton);
			container.Register<IBuildVariable, BuildSqlVariable>(Lifestyle.Singleton);
			container.Register<IBuildParameter, BuildProcedureParameter>(Lifestyle.Singleton);
			container.Register<IStoreVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<IGetVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<ICreateVariableName, CreateSqlVariableName>(Lifestyle.Singleton);

			container.Register<ICodeGenerator<CSharp.Model.Property>, WriteCSharpProperty>(Lifestyle.Singleton);
			container.Register<ICodeGenerator<CSharp.Model.DotNetType>, WriteDotNetType>(Lifestyle.Singleton);
			container.Register<ICodeGenerator<CSharp.Model.AccessModifier>, WriteAccessModifier >(Lifestyle.Singleton);
			container.Register<ICodeGenerator<CSharp.Model.Class>, WriteCSharpClass>(Lifestyle.Singleton);
			container.Register<ICodeGenerator<CSharp.Model.Constructor>, WriteConstructor>(Lifestyle.Singleton);
			container.Register<ICodeGenerator<CSharp.Model.Variable>, WriteParameter>(Lifestyle.Singleton);
			container.Register<ICodeGenerator<CSharp.Model.Method>, WriteMethod>(Lifestyle.Singleton);
			container.Register<ICodeGenerator<CSharp.Model.Field>, WriteField>(Lifestyle.Singleton);


            container.Register<IConvertObject<Type, CSharp.Model.Class>, ConvertTypeToCSharpClass>(Lifestyle.Singleton);
            container.Register<IConvertObject<Type, TypeScript.Model.Class>, TypeScript.Conversion.ConvertTypeToTypeScriptClass>(Lifestyle.Transient);
            container.Register<IConvertObject<PropertyInfo, CSharp.Model.Property>, ConvertPropertyInfoToProperty>(Lifestyle.Singleton);
            container.Register<IConvertObject<FieldInfo, CSharp.Model.Field>, ConvertFieldInfoToField>(Lifestyle.Singleton);
            container.Register<IConvertObject<Type, CSharp.Model.DotNetType>, ConvertTypeToDotNetType>(Lifestyle.Singleton);
		}
	}
}
