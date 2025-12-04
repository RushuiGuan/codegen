using Albatross.CodeGen.CSharp.Expressions;
using System;

namespace Albatross.CodeGen.CSharp.Declarations {
	public static class Extensions {
		public static ClassDeclaration AddIf<T>(this ClassDeclaration classDeclaration, bool condition, Func<T> func) {
			if(condition) {
				classDeclaration.Add(func);
			}
			return classDeclaration;
		}
		public static ClassDeclaration Add<T>(this ClassDeclaration classDeclaration, Func<T> func) {
			var t = func();
			switch(t) {
				case ConstructorDeclaration constructorDeclaration:
					classDeclaration.Constructors.Add(constructorDeclaration);
					break;
				case PropertyDeclaration propertyDeclaration:
					classDeclaration.Properties.Add(propertyDeclaration);
					break;
				case FieldDeclaration fieldDeclaration:
					classDeclaration.Fields.Add(fieldDeclaration);
					break;
				case MethodDeclaration methodDeclaration:
					classDeclaration.Methods.Add(methodDeclaration);
					break;
				case AttributeExpression attributeExpression:
					classDeclaration.AttributeExpressions.Add(attributeExpression);
					break;
				default:
					throw new InvalidOperationException($"Unsupported type: {typeof(T).FullName}");
			}
			return classDeclaration;
		}
	}
}
