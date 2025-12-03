using Albatross.CodeGen.Syntax;
using System;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public class InvocationExpressionBuilder : ExpressionBuilder<InvocationExpression> {
		private bool useAwaitOperator;
		private IExpression? callableExpression;
		private IIdentifierNameExpression? chained;
		private readonly List<IExpression> arguments = new List<IExpression>();
		private readonly List<ITypeExpression> genericArguments = new List<ITypeExpression>();

		public InvocationExpressionBuilder WithIdentifier(IIdentifierNameExpression identifier) {
			this.callableExpression = identifier;
			return this;
		}

		public InvocationExpressionBuilder WithFunctionName(string name) {
			this.callableExpression = new IdentifierNameExpression(name);
			return this;
		}

		public InvocationExpressionBuilder WithMultiPartFunctionName(params string[] names) {
			this.callableExpression = new MultiPartIdentifierNameExpression(names);
			return this;
		}

		public InvocationExpressionBuilder WithCallableExpression(IExpression expression) {
			this.callableExpression = expression;
			return this;
		}

		public InvocationExpressionBuilder AddGenericArgument(ITypeExpression typeExpression) {
			this.genericArguments.Add(typeExpression);
			return this;
		}

		public InvocationExpressionBuilder AddArgument(IExpression expression) {
			if (expression is not Syntax.NoOpExpression) {
				this.arguments.Add(expression);
			}
			return this;
		}

		public InvocationExpressionBuilder Await(bool useAwait = true) {
			this.useAwaitOperator = useAwait;
			return this;
		}

		public override InvocationExpression Build() {
			return new InvocationExpression() {
				UseAwaitOperator = useAwaitOperator,
				CallableExpression = callableExpression ?? throw new InvalidOperationException("Name is not set"),
				Chained = chained,
				ArgumentList = new ListOfSyntaxNodes<IExpression>(arguments),
				GenericArguments = new ListOfSyntaxNodes<ITypeExpression>(genericArguments),
			};
		}

		public InvocationExpressionBuilder Chain(string functionName) {
			return new InvocationExpressionBuilder {
				callableExpression = this.Build(),
				chained = new IdentifierNameExpression(functionName)
			};
		}
	}
}