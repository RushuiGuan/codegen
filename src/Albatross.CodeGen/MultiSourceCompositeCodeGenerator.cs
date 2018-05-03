using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen {
	public class MultiSourceCompositeCodeGenerator : ICodeGenerator<object, object> {
		ICodeGeneratorFactory factory;
		Branch branch;
		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }

		public MultiSourceCompositeCodeGenerator(ICodeGeneratorFactory factory) {
			this.factory = factory;
		}

		public IEnumerable<object> Build(StringBuilder sb, object source, object option) {
			Queue<ICodeGenerator> queue = new Queue<ICodeGenerator>();
			List<object> list = new List<object>();

			foreach (var item in branch) {
				if (item is Leaf leaf) {
					object leafSource = leaf.Source ?? source;
					var gen = factory.Create(leaf.Name);

					gen.Yield += (scoped_sb) => OnYield(queue, scoped_sb, leafSource, option);
					queue.Enqueue(gen);
				} else {
					var gen = new MultiSourceCompositeCodeGenerator(factory);
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

		private IEnumerable<object> OnYield(Queue<ICodeGenerator> queue, StringBuilder sb, object source, object option) {
			if (queue.Count > 0) {
				var handle = queue.Dequeue();
				return handle.Build(sb, source, option);
			} else {
				return new object[0];
			}
		}
	}
}
