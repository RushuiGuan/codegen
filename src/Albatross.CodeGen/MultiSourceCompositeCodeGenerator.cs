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

		public IEnumerable<object> Generate(StringBuilder sb, IDictionary<string, string> customCode, object source, object option) {
			Queue<INode> queue = new Queue<INode>();
			List<object> list = new List<object>();

			foreach (var item in branch) {
				if (item is Leaf leaf) {
					if(leaf.Source == null) { leaf.Source = source; }
					if(leaf.Option == null) { leaf.Option = option; }
					leaf.CodeGenerator = factory.Create(leaf.Name);
					leaf.CodeGenerator.Yield += (scoped_sb) => OnGreedyYield(queue, scoped_sb, customCode);
					queue.Enqueue(leaf);
				} else {
					Branch branch = (Branch)item;
					if(branch.Source == null) { branch.Source = source; }
					if(branch.Option == null) { branch.Option = option; }
					branch.CodeGenerator = new MultiSourceCompositeCodeGenerator(factory);
					branch.CodeGenerator.Configure(item);
					queue.Enqueue(branch);
				}
			}

			while (queue.Count > 0) {
				var node = queue.Dequeue();
				var used = node.CodeGenerator.Generate(sb, customCode, node.Source, node.Option);
				list.AddRange(used);
			}
			return list;
		}

		public void Configure(object data) {
			branch = data as Branch;
		}

		private IEnumerable<object> OnGreedyYield(Queue<INode> queue, StringBuilder sb, IDictionary<string, string> customCode) {
			List<object> list = new List<object>();

			while(queue.Count > 0) {
				INode node = queue.Dequeue();
				var items = node.CodeGenerator.Generate(sb, customCode, node.Source, node.Option);
				list.AddRange(items);
			}
			return list;
		}
		private IEnumerable<object> OnYield(Queue<INode> queue, StringBuilder sb, IDictionary<string, string> customCode) {
			if (queue.Count > 0) {
				INode node = queue.Dequeue();
				return node.CodeGenerator.Generate(sb, customCode, node.Source, node.Option);
			} else {
				return new object[0];
			}
		}
	}
}
