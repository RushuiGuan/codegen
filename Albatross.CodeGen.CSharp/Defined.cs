using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;

namespace Albatross.CodeGen.CSharp {
	/// <summary>
	/// Provides pre-defined C# code generation elements including types, identifiers, namespaces, literals, and operators
	/// </summary>
	public static class Defined {
		/// <summary>
		/// Pre-defined C# type expressions for common .NET types
		/// </summary>
		public static class Types {
			public static readonly ITypeExpression Int = new TypeExpression("int");
			public static readonly ITypeExpression Long = new TypeExpression("long");
			public static readonly ITypeExpression String = new TypeExpression("string");
			public static readonly ITypeExpression NullableString = new TypeExpression("string") {
				Nullable = true
			};
			public static readonly ITypeExpression Bool = new TypeExpression("bool");
			public static readonly ITypeExpression Double = new TypeExpression("double");
			public static readonly ITypeExpression Float = new TypeExpression("float");
			public static readonly ITypeExpression Decimal = new TypeExpression("decimal");
			public static readonly ITypeExpression Object = new TypeExpression("object");
			public static readonly ITypeExpression NullableObject = new TypeExpression("object") {
				Nullable = true
			};

			public static readonly ITypeExpression Void = new TypeExpression("void");
			public static readonly ITypeExpression Var = new TypeExpression("var");
			public static readonly ITypeExpression Task = new TypeExpression(Identifiers.Task);
			public static readonly ITypeExpression Char = new TypeExpression("char");

			public static readonly ITypeExpression NameValueCollection = new TypeExpression(Identifiers.NameValueCollection);
			public static readonly TypeExpression IServiceCollection = new(Identifiers.IServiceCollection);
			public static readonly ITypeExpression DateTime = new TypeExpression(Identifiers.DateTime);
			public static readonly ITypeExpression DateOnly = new TypeExpression(Identifiers.DateOnly);
		}

		/// <summary>
		/// Pre-defined identifier name expressions for common .NET types and members
		/// </summary>
		public static class Identifiers {
			public static readonly IIdentifierNameExpression Task = new QualifiedIdentifierNameExpression("Task", Defined.Namespaces.SystemThreadingTasks);
			public static readonly IIdentifierNameExpression NameValueCollection = new QualifiedIdentifierNameExpression("NameValueCollection", Defined.Namespaces.SystemCollectionsSpecialized);
			public static readonly QualifiedIdentifierNameExpression IServiceCollection = new("IServiceCollection", Namespaces.MicrosoftExtensionsDependencyInjection);
			public static readonly QualifiedIdentifierNameExpression DateTime = new("DateTime", Namespaces.System);
			public static readonly QualifiedIdentifierNameExpression DateOnly = new("DateOnly", Namespaces.System);
			public static readonly IdentifierNameExpression Disgard = new("_");
		}

		/// <summary>
		/// Pre-defined namespace expressions for common .NET namespaces
		/// </summary>
		public static class Namespaces {
			public static readonly NamespaceExpression System = new("System");
			public static readonly NamespaceExpression SystemThreadingTasks = new("System.Threading.Tasks");
			public static readonly NamespaceExpression SystemCollectionsSpecialized = new("System.Collections.Specialized");
			public static readonly NamespaceExpression MicrosoftExtensionsDependencyInjection = new("Microsoft.Extensions.DependencyInjection");
		}

		/// <summary>
		/// Pre-defined literal expressions for common C# literal values
		/// </summary>
		public static class Literals {
			public static readonly IExpression Null = new NullExpression();
			public static readonly IExpression True = new BooleanLiteralExpression(true);
			public static readonly IExpression False = new BooleanLiteralExpression(false);
		}

		/// <summary>
		/// Pre-defined operator expressions for common C# operators
		/// </summary>
		public static class Operators {
			public static readonly IOperator NotEqual = new Operator("!=");
			public static readonly IOperator Equal = new Operator("==");
			public static readonly IOperator GreaterThan = new Operator(">");
			public static readonly IOperator LessThan = new Operator("<");
			public static readonly IOperator GreaterThanOrEqual = new Operator(">=");
			public static readonly IOperator LessThanOrEqual = new Operator("<=");
			public static readonly IOperator Plus = new Operator("+");
			public static readonly IOperator Minus = new Operator("-");
			public static readonly IOperator Assignment = new Operator("=");
		}

		public static class PreprocessorDirectives {
			public static readonly PreprocessorDirectiveExpression Region = new PreprocessorDirectiveExpression("region");
			public static readonly PreprocessorDirectiveExpression EndRegion = new PreprocessorDirectiveExpression("endregion");
			public static readonly PreprocessorDirectiveExpression NullableEnable = new PreprocessorDirectiveExpression("nullable enable");
			public static readonly PreprocessorDirectiveExpression NullableDisable = new PreprocessorDirectiveExpression("nullable disable");
			public static readonly PreprocessorDirectiveExpression NullableRestore = new PreprocessorDirectiveExpression("nullable restore");
		}

		public static class Keywords {
			public static readonly AccessModifierKeyword Public = new("public");
			public static readonly AccessModifierKeyword Private = new AccessModifierKeyword("private");
			public static readonly AccessModifierKeyword Protected = new AccessModifierKeyword("protected");
			public static readonly AccessModifierKeyword Internal = new AccessModifierKeyword("internal");

			public static readonly IKeyword Abstract = new Keyword("abstract");
			public static readonly IKeyword Async = new Keyword("async");
			public static readonly IKeyword Await = new Keyword("await");
			public static readonly IKeyword Extern = new Keyword("extern");
			public static readonly IKeyword Const = new Keyword("const");
			public static readonly IKeyword Override = new Keyword("override");
			public static readonly IKeyword Sealed = new Keyword("sealed");
			public static readonly IKeyword Static = new Keyword("static");
			public static readonly IKeyword Virtual = new Keyword("virtual");
			public static readonly IKeyword Partial = new Keyword("partial");
			public static readonly IKeyword Readonly = new Keyword("readonly");
			public static readonly IKeyword Return = new Keyword("return");
			public static readonly IKeyword ForEach = new Keyword("foreach");
			public static readonly IKeyword In = new Keyword("in");
			public static readonly IKeyword If = new Keyword("if");
			public static readonly IKeyword Else = new Keyword("else");
			public static readonly IKeyword Using = new Keyword("using");
			public static readonly IKeyword This = new Keyword("this");
			public static readonly IKeyword Namespace = new Keyword("namespace");
			public static readonly IKeyword Record = new Keyword("record");
			public static readonly IKeyword Class = new Keyword("class");
			public static readonly IKeyword New = new Keyword("new");
			public static readonly IKeyword Throw = new Keyword("throw");

			public static readonly IKeyword Set = new Keyword("set") {
				PadRight = false
			};

			public static readonly IKeyword Get = new Keyword("get") {
				PadRight = false
			};
		}

		public static class Attributes {
			public static readonly AttributeExpression Obsolete = new AttributeExpression {
				CallableExpression = new IdentifierNameExpression("System.ObsoleteAttribute")
			};
		}
	}
}