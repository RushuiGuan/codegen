using Albatross.CodeGen.Core;
using Albatross.CodeGen.Faults;
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


		public IEnumerable<object> Run(CodeGenerator meta, StringBuilder sb, object source, ICodeGeneratorOption option) {
			if (option == null) { option = (ICodeGeneratorOption)Activator.CreateInstance(meta.OptionType); }
			ICodeGenerator gen = (ICodeGenerator)objectFactory.Create(meta.GeneratorType);
			gen.Configure(meta.Data);
			return gen.Generate(sb, source, option);
		}
	}
}
