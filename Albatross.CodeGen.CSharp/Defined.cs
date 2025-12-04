using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.CSharp {
	public static class Defined {
		public static class Types {
			public static readonly ITypeExpression Int = new TypeExpression("int");
			public static readonly ITypeExpression Long = new TypeExpression("long");
			public static readonly ITypeExpression String = new TypeExpression("string");
			public static readonly ITypeExpression Bool = new TypeExpression("bool");
			public static readonly ITypeExpression Double = new TypeExpression("double");
			public static readonly ITypeExpression Float = new TypeExpression("float");
			public static readonly ITypeExpression Decimal = new TypeExpression("decimal");
			public static readonly ITypeExpression Object = new TypeExpression("object");
			public static readonly ITypeExpression Void = new TypeExpression("void");
			public static readonly ITypeExpression Var = new TypeExpression("var");
		}
	}
}