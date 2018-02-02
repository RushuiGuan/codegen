using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen {
	public class CompositeCodeGenerator<T, O> : ICodeGenerator<T, O> {
		IEnumerable<string> _generators;

		public CompositeCodeGenerator(Composite<T, O> composite) {
			_generators = composite.Generators;
		}

		public CodeGenerator Meta { get; private set; } = new CodeGenerator();
		public event Func<StringBuilder, T, O, ICodeGeneratorFactory, IEnumerable<object>> Yield { add { } remove { } }

		public StringBuilder Build(StringBuilder sb, T source, O option, ICodeGeneratorFactory factory, out IEnumerable<object> generators) {
			Queue<ICodeGenerator<T, O>> queue = new Queue<ICodeGenerator<T, O>>();
			List<object> list = new List<object>();

			foreach (var item in _generators) {
				var gen = factory.Create<T, O>(item);
				gen.Yield += (scoped_sb, scoped_source, scoped_option, scoped_factory)=> Gen_Yield(queue, scoped_sb,  scoped_source, scoped_option, scoped_factory);
				queue.Enqueue(gen);
			}

			while (queue.Count > 0) {
				var item = queue.Dequeue();
				item.Build(sb, source, option, factory, out IEnumerable<object> scoped_generators);
				list.AddRange(scoped_generators);
			}
			generators = list;
			return sb;
		}

		private IEnumerable<object> Gen_Yield(Queue<ICodeGenerator<T, O>> queue, StringBuilder sb, T source, O option, ICodeGeneratorFactory factory) {

			if (queue.Count > 0) {
				var item = queue.Dequeue();
				item.Build(sb, source, option, factory, out IEnumerable<object> list);
				return list;
			} else {
				return new object[0];
			}
		}
	}
}
