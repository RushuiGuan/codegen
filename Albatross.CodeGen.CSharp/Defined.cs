using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.CSharp {
	public static class Defined {
		public static class Types {
			public static readonly ITypeExpression Int = new SimpleTypeExpression("int");
			public static readonly ITypeExpression Long = new SimpleTypeExpression("long");
			public static readonly ITypeExpression String = new SimpleTypeExpression("string");
			public static readonly ITypeExpression Bool = new SimpleTypeExpression("bool");
			public static readonly ITypeExpression Double = new SimpleTypeExpression("double");
			public static readonly ITypeExpression Float = new SimpleTypeExpression("float");
			public static readonly ITypeExpression Decimal = new SimpleTypeExpression("decimal");
			public static readonly ITypeExpression Object = new SimpleTypeExpression("object");
			public static readonly ITypeExpression Void = new SimpleTypeExpression("void");
			public static readonly ITypeExpression Var = new SimpleTypeExpression("var");
		}
	}
}