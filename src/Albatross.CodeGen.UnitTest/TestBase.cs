﻿using Albatross.CodeGen.Autofac;
using Autofac;
using NUnit.Framework;

namespace Albatross.CodeGen.UnitTest {
	public class TestBase {
		protected IContainer container;

		[OneTimeSetUp]
		public void OneTimeSetUp() {
			ContainerBuilder builder = new ContainerBuilder();
			new Pack().Register(builder);
			container = builder.Build();
		}
	}
}