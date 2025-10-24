﻿using Albatross.CodeGen.Syntax;
using System;

namespace Albatross.CodeGen.Python.Expressions {
	public class ScopedVariableExpressionBuilder : ExpressionBuilder<ScopedVariableExpression> {
		private string? name;
		private ITypeExpression? type;
		private IExpression? expression;

		public ScopedVariableExpressionBuilder WithName(string name) {
			this.name = name;
			return this;
		}

		public ScopedVariableExpressionBuilder WithType(ITypeExpression type) {
			this.type = type;
			return this;
		}

		public ScopedVariableExpressionBuilder WithExpression(IExpression expression) {
			this.expression = expression;
			return this;
		}

		public override ScopedVariableExpression Build()
			=> new ScopedVariableExpression {
				Identifier = new IdentifierNameExpression(name ?? throw new InvalidOperationException("Variable name has not been set")),
				Type = this.type,
				Assignment = expression,
			};
	}
}