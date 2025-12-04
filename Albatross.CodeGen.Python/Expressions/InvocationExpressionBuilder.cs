using Albatross.CodeGen.Syntax;
using System;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.Expressions {
	public class InvocationSyntaxNodeBuilder : SyntaxNodeBuilder<InvocationExpression> {
		private bool useAwaitOperator;
		private IExpression? callableExpression;
		private IIdentifierNameExpression? chained;
		private readonly List<IExpression> arguments = new List<IExpression>();
		private readonly List<ITypeExpression> genericArguments = new List<ITypeExpression>();

		public InvocationSyntaxNodeBuilder WithIdentifier(IIdentifierNameExpression identifier) {
			this.callableExpression = identifier;
			return this;
		}

		public InvocationSyntaxNodeBuilder WithFunctionName(string name) {
			this.callableExpression = new IdentifierNameExpression(name);
			return this;
		}

		public InvocationSyntaxNodeBuilder WithMultiPartFunctionName(params string[] names) {
			this.callableExpression = new MultiPartIdentifierNameExpression(names);
			return this;
		}

		public InvocationSyntaxNodeBuilder WithCallableExpression(IExpression expression) {
			this.callableExpression = expression;
			return this;
		}

		public InvocationSyntaxNodeBuilder AddGenericArgument(ITypeExpression typeExpression) {
			this.genericArguments.Add(typeExpression);
			return this;
		}

		public InvocationSyntaxNodeBuilder AddArgument(IExpression expression) {
			if (expression is not NoOpExpression) {
				this.arguments.Add(expression);
			}
			return this;
		}

		public InvocationSyntaxNodeBuilder Await(bool useAwait = true) {
			this.useAwaitOperator = useAwait;
			return this;
		}

		public override InvocationExpression Build() {
			return new InvocationExpression() {
				UseAwaitOperator = useAwaitOperator,
				CallableExpression = callableExpression ?? throw new InvalidOperationException("Name is not set"),
				Chained = chained,
				ArgumentList = new ListOfSyntaxNodes<IExpression> { Nodes = arguments },
				GenericArguments = new ListOfSyntaxNodes<ITypeExpression> { Nodes = genericArguments },
			};
		}

		public InvocationSyntaxNodeBuilder Chain(string functionName) {
			return new InvocationSyntaxNodeBuilder {
				callableExpression = this.Build(),
				chained = new IdentifierNameExpression(functionName)
			};
		}
	}
}