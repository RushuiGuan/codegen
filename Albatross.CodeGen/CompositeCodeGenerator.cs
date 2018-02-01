using Albatross.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CompositeCodeGenerator<T, O> : ICodeGenerator<T, O> {
		IEnumerable<string> _generators;

		public CompositeCodeGenerator(Composite<T, O> composite) {
			_generators = composite.Generators;
		}

		public string Seperator { get; set; }
		public CodeGenerator Meta { get; private set; } = new CodeGenerator();

		public IEnumerable<ICodeGenerator<T, O>> Children { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public StringBuilder Build(StringBuilder sb, T t, O options, ICodeGeneratorFactory factory) {
			foreach (var item in _generators) {
				var gen = factory.Get<T, O>(item);
				gen.Build(sb, t, options, factory);
				if (item != _generators.Last()) {
					sb.Append(Seperator);
				}
			}
			return sb;
		}
	}
}
