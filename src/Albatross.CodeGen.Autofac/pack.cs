﻿using System;
using Albatross.CodeGen.Core;
using Autofac;
using Autofac.Features.ResolveAnything;

namespace Albatross.CodeGen.Autofac {
	public class Pack {
		public void Register(ContainerBuilder containerBuilder) {
			var asm = typeof(Albatross.CodeGen.Core.ICodeGenerator).Assembly;
			containerBuilder.RegisterAssemblyTypes(asm)
				.AsClosedTypesOf(typeof(IConvertObject<>))
				.AsClosedTypesOf(typeof(IConvertObject<,>));

			containerBuilder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
		}
	}
}
