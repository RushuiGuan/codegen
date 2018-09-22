using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen {
	public class CompositeCodeGenerator : CodeGeneratorBase<object, object> {
		ICodeGeneratorFactory factory;
		Branch branch;

		public CompositeCodeGenerator(ICodeGeneratorFactory factory) {
			this.factory = factory;
		}

		public override IEnumerable<object> Build(StringBuilder sb, object source, object option) {
			Queue<ICodeGenerator> queue = new Queue<ICodeGenerator>();
			List<object> list = new List<object>();

			foreach (var item in branch) {
				if (item.IsLeaf) {
					var gen = factory.Create(item.Name);
					gen.Yield += (scoped_sb) => OnYield(queue, scoped_sb, source, option);
					queue.Enqueue(gen);
				} else {
					var gen = new CompositeCodeGenerator(factory);
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

		public override void Configure(object data) {
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
