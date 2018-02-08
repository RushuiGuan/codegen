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
			Type sourceType = source?.GetType();
			if (sourceType != meta.SourceType) { throw new InvalidSourceTypeException(); }
			if (option == null) {
				option = Activator.CreateInstance(meta.OptionType);
			} else {
				if (meta.OptionType != option.GetType()) { throw new InvalidOptionTypeException(); }
			}
			object gen = objectFactory.Create(meta.GeneratorType);
			MethodInfo method = meta.GeneratorType.GetMethod(nameof(ICodeGenerator<object, object>.Configure));
			method.Invoke(gen, new[] { meta.Data });
			method = meta.GeneratorType.GetMethod(nameof(ICodeGenerator<object, object>.Build));
			var used = method.Invoke(gen, new[] { sb, source, option }) as IEnumerable<object>;
			if (used == null) { used = new object[0]; }
			return used;
		}
	}
}
