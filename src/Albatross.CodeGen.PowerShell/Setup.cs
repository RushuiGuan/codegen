using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;
using System;

namespace Albatross.CodeGen.PowerShell {
	public class Setup {

		private IObjectFactory Run() {
			Container container = new Container();
			container.Register<IObjectFactory>(() => new ObjectFactory(container), Lifestyle.Singleton);

			new Albatross.CodeGen.Ioc.SimpleInjectorPackage().RegisterServices(container);
			new Albatross.Database.Ioc.SimpleInjectorSqlServerPackage().RegisterServices(container);
			container.Verify();


			IFactory<SourceType> factory = container.GetInstance<IFactory<SourceType>>();
			factory.Register(new SourceType {
				 ObjectType = typeof(Albatross.Database.Table),
				 Description = "A Database Table",
			});

			factory.Register(new SourceType {
				ObjectType = typeof(Albatross.Database.Procedure),
				Description = "A Database Stored Procedure",
			});

			factory.Register(new SourceType {
				ObjectType = typeof(Albatross.Database.Database),
				Description = "A Database",
			});

			return container.GetInstance<IObjectFactory>();
		}

		static Lazy<IObjectFactory> _lazy = new Lazy<IObjectFactory>(() => new Setup().Run());
		public static IObjectFactory Factory { get; private set; } = _lazy.Value;
	}
}
