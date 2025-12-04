using System;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.Syntax {
	public abstract class SyntaxNodeBuilder<T> : SyntaxNodeBuilder where T : ISyntaxNode {
		public abstract T Build();
		protected sealed override ISyntaxNode InternalBuild() => this.Build();
	}

	public abstract class SyntaxNodeBuilder {
		private Queue<Func<ISyntaxNode>> queue;

		public SyntaxNodeBuilder() {
			this.queue = new Queue<Func<ISyntaxNode>>();
			queue.Enqueue(this.InternalBuild);
		}

		public Builder Next<Builder>() where Builder : SyntaxNodeBuilder, new() {
			var next = new Builder();
			this.queue.Enqueue(next.InternalBuild);
			next.queue = this.queue;
			return next;
		}
		public SyntaxNodeBuilder Add(SyntaxNodeBuilder builder) {
			this.queue.Enqueue(builder.InternalBuild);
			return this;
		}
		public SyntaxNodeBuilder Add(Func<ISyntaxNode> func) {
			this.queue.Enqueue(func);
			return this;
		}
		protected abstract ISyntaxNode InternalBuild();
		public CompositeExpression BuildAll() => new CompositeExpression(this.queue.Select(x => x()).ToArray());
	}
}