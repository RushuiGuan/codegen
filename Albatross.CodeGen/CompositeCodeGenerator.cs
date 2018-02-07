﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albatross.CodeGen {
	public class CompositeCodeGenerator<T, O> : ICodeGenerator<T, O> {
		Branch branch;

		public event Func<StringBuilder, ICodeGeneratorFactory, IEnumerable<object>> Yield { add { }remove { } }

		public StringBuilder Build(StringBuilder sb, T source, O option, ICodeGeneratorFactory factory, out IEnumerable<object> generators) {
			Queue<ICodeGenerator<T, O>> queue = new Queue<ICodeGenerator<T, O>>();
			List<object> list = new List<object>();

			foreach (var item in branch) {
				if (item.IsLeaf) {
					var gen = factory.Create<T, O>(item.Name);
					gen.Yield += (scoped_sb, scoped_factory) => OnYield(queue, scoped_sb, source, option, scoped_factory);
					queue.Enqueue(gen);
				} else {
					var gen = new CompositeCodeGenerator<T, O>();
					gen.Configure(item);
					queue.Enqueue(gen);
				}
			}

			while (queue.Count > 0) {
				var item = queue.Dequeue();
				item.Build(sb, source, option, factory, out IEnumerable<object> scoped_generators);
				list.AddRange(scoped_generators);
			}
			generators = list;
			return sb;
		}

		public void Configure(object data) {
			branch = data as Branch;

		}

		private IEnumerable<object> OnYield(Queue<ICodeGenerator<T, O>> queue, StringBuilder sb, T source, O option, ICodeGeneratorFactory factory) {
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
