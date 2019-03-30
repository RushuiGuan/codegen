using System;
using Albatross.CodeGen.Core;
using Autofac;

namespace Albatross.CodeGen.Autofac {
	public class Pack {
		public void Register(ContainerBuilder containerBuilder) {
			var asm = typeof(Albatross.CodeGen.Core.ICodeGenerator).Assembly;
			}
	}
}
