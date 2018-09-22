using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen
{
	public class RunCodeGenerator : IRunCodeGenerator {
		IObjectFactory objectFactory;
		ICodeGeneratorFactory codeGeneratorFactory;

		public RunCodeGenerator(IObjectFactory objectFactory, ICodeGeneratorFactory codeGeneratorFactory) {
			this.objectFactory = objectFactory;
			this.codeGeneratorFactory = codeGeneratorFactory;
		}

		public IEnumerable<object> Run(CodeGenerator meta, StringBuilder sb, object source, object option) {
			ICodeGenerator gen = (ICodeGenerator)objectFactory.Create(meta.GeneratorType);
			gen.Configure(meta.Data);
			
			var used = gen.Build(sb, source, option);
			if (used == null) { used = new object[0]; }
			return used;
		}
	}
}
