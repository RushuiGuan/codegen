using Autofac;
using Autofac.Features.ResolveAnything;
using NUnit.Framework;
using SimpleInjector;
using System;

namespace Albatross.Test {
	public abstract class TestBase {
		Container container = new Container();

		public virtual void Build(Autofac.ContainerBuilder builder) {
			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
		}

		public IContainer GetContainer() {
			Autofac.ContainerBuilder builder = new ContainerBuilder();
			Build(builder);
			return builder.Build(Autofac.Builder.ContainerBuildOptions.None);
		}
		public ILifetimeScope GetScope() {
			return GetContainer().BeginLifetimeScope();
		}


		public abstract void Register(Container container);

		[OneTimeSetUp]
		public void Init() {
			Register(container);
			container.Verify();
		}

		[OneTimeTearDown]
		public void Teardown() {
			container.Dispose();
		}

		protected T Get<T>() where T:class{
			return container.GetInstance<T>();
		}
	}
}
