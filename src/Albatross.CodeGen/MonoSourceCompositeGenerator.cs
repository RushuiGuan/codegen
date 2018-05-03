using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
	public class MonoSourceCompositeCodeGenerator<T, O> : ICodeGenerator<T, O>  where T:class where O:class{
		ICodeGeneratorFactory factory;
		Branch branch;
		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }

		public MonoSourceCompositeCodeGenerator(ICodeGeneratorFactory factory) {
			this.factory = factory;
		}

		public IEnumerable<object> Build(StringBuilder sb, T source, O option) {
			Queue<ICodeGenerator<T, O>> queue = new Queue<ICodeGenerator<T, O>>();
			List<object> list = new List<object>();

			foreach (var item in branch) {
				if (item is Leaf leaf) {
					var gen = factory.Create<T, O>(item.Name);
					gen.Yield += (scoped_sb) => OnYield(queue, scoped_sb, source, option);
					queue.Enqueue(gen);
				} else {
					var gen = new MonoSourceCompositeCodeGenerator<T, O>(factory);
					gen.Configure(item);
					queue.Enqueue(gen);
				}
			}

			while (queue.Count > 0) {
				var item = queue.Dequeue();
				var used = item.Build(sb, source, option);
				list.AddRange(used);
			}
			return list;
		}

		public void Configure(object data) {
			branch = data as Branch;

		}

		private IEnumerable<object> OnYield(Queue<ICodeGenerator<T, O>> queue, StringBuilder sb, T source, O option) {
			if (queue.Count > 0) {
				var handle = queue.Dequeue();
				return handle.Build(sb, source, option);
			} else {
				return new object[0];
			}
		}

		public IEnumerable<object> Build(StringBuilder sb, object source, object option) {
			return this.ValidateNBuild(sb, source, option);
		}
	}
}