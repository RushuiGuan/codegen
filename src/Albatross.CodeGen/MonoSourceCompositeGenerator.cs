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

		public IEnumerable<object> Generate(StringBuilder sb, T source, O option) {
			Queue<ICodeGenerator<T, O>> queue = new Queue<ICodeGenerator<T, O>>();
			List<object> list = new List<object>();

			foreach (var item in branch) {
				if (item is Leaf leaf) {
					var gen = factory.Create<T, O>(item.Name);
					gen.Yield += (scoped_sb) => OnGreedyYield(queue, scoped_sb, source, option);
					queue.Enqueue(gen);
				} else {
					var gen = new MonoSourceCompositeCodeGenerator<T, O>(factory);
					gen.Configure(item);
					queue.Enqueue(gen);
				}
			}

			while (queue.Count > 0) {
				var item = queue.Dequeue();
				var used = item.Generate(sb, source, option);
				list.AddRange(used);
			}
			return list;
		}

		public void Configure(object data) {
			branch = data as Branch;

		}

		private IEnumerable<object> OnGreedyYield(Queue<ICodeGenerator<T, O>> queue, StringBuilder sb, T source, O option) {
			List<object> list = new List<object>();
			while (queue.Count > 0) {
				var handle = queue.Dequeue();
				var items = handle.Generate(sb, source, option);
				list.AddRange(items);
			}
			return list;

			/*
			if (queue.Count > 0) {
				var handle = queue.Dequeue();
				return handle.Build(sb, source, option);
			} else {
				return new object[0];
			}
			*/
		}
		private IEnumerable<object> OnSingleYield(Queue<ICodeGenerator<T, O>> queue, StringBuilder sb, T source, O option) {
			if (queue.Count > 0) {
				var handle = queue.Dequeue();
				return handle.Generate(sb, source, option);
			} else {
				return new object[0];
			}
		}

		public IEnumerable<object> Generate(StringBuilder sb, object source, object option) {
			return this.ValidateNGenerate(sb, source, option);
		}
	}
}