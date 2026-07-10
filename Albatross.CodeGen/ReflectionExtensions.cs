using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Albatross.CodeGen {
	// TODO: these methods are copied from Albatross.Reflection so that we don't have to take on the dependency.
	public static class ReflectionExtensions {
		/// <summary>
		/// Determines whether the specified type is a concrete class (not abstract, not interface, not generic type definition).
		/// </summary>
		/// <param name="type">The type to check</param>
		/// <returns>True if the type is a concrete class; otherwise, false</returns>
		public static bool IsConcreteType(this Type type) => !type.IsAbstract && !type.IsInterface && type.IsClass && !type.IsGenericTypeDefinition;
		/// <summary>
		/// Provide with a class type and a generic type\interface definition, this methods will return true if the class derives\implements the generic type\interface.  It will
		/// also output the generic type.
		/// 
		/// Example:
		/// public class Test: IEnumerable&lt;int&gt; { }
		/// 
		/// var result = typeof(Test).TryGetClosedGenericTypes(typeof(IEnumerable&lt;&gt;), out Type type);
		/// Assert.True(result);
		/// Assert.AreSame(typeof(IEnumerable&lt;int&gt;), type);
		/// </summary>
		/// <param name="type">Input class type</param>
		/// <param name="genericDefinition">The definition of a generic type.  For example: typeof(IEnumerable&lt;&gt;)</param>
		/// <param name="genericType">If the class extends\implements the generic type\interface, its type will be set in this output parameter</param>
		/// <returns>Return true if the class implements the generic interface</returns>
		public static bool TryGetClosedGenericType(this Type type, Type genericDefinition, [NotNullWhen(true)] out Type? genericType) {
			genericType = null;
			if (!type.IsAbstract && type.IsClass && !type.IsGenericTypeDefinition) {
				if (genericDefinition.IsInterface) {
					genericType = type.GetInterfaces().FirstOrDefault(args => args.IsGenericType && args.GetGenericTypeDefinition() == genericDefinition);
				} else {
					while (type != typeof(object)) {
						if (type.IsGenericType && type.GetGenericTypeDefinition() == genericDefinition) {
							genericType = type;
							break;
						}
						type = type.BaseType ?? typeof(object);
					}
				}
			}
			return genericType != null;
		}
		/// <summary>
		/// Determines whether the specified type is derived from or implements the generic type T.
		/// </summary>
		/// <typeparam name="T">The base type or interface to check against</typeparam>
		/// <param name="type">The type to check</param>
		/// <returns>True if the type derives from or implements type T; otherwise, false</returns>
		public static bool IsDerived<T>(this Type type) => typeof(T).IsAssignableFrom(type);
	}
}