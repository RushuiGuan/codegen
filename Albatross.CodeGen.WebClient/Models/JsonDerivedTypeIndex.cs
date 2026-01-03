using Albatross.CodeAnalysis;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.Models {
	public interface IJsonDerivedTypeIndex {
		(ITypeSymbol, string)[] GetDerivedTypeDiscriminators(ITypeSymbol symbol);
	}
	public sealed class JsonDerivedTypeIndex : IJsonDerivedTypeIndex {
		public JsonDerivedTypeIndex(IProjectCompilationFactory factory) {
			this.compilation = factory.Get();
		}

		readonly ConcurrentDictionary<ITypeSymbol, IReadOnlyDictionary<ITypeSymbol, string>> indexes = new ConcurrentDictionary<ITypeSymbol, IReadOnlyDictionary<ITypeSymbol, string>>(SymbolEqualityComparer.Default);
		readonly ConcurrentDictionary<ITypeSymbol, (ITypeSymbol, string)[]> cache = new ConcurrentDictionary<ITypeSymbol, (ITypeSymbol, string)[]>(SymbolEqualityComparer.Default);
		private readonly Compilation compilation;

		public (ITypeSymbol, string)[] GetDerivedTypeDiscriminators(ITypeSymbol symbol) {
			if (cache.TryGetValue(symbol, out var cachedItems)) {
				return cachedItems;
			}
			var list = new List<(ITypeSymbol, string)>();
			foreach (var @interface in symbol.AllInterfaces) {
				var discriminators = GetTypeDiscriminators(@interface);
				if (discriminators.TryGetValue(symbol, out var discriminator)) {
					list.Add((@interface, discriminator));
				}
			}
			for (var baseType = symbol.BaseType; baseType != null; baseType = baseType.BaseType) {
				var discriminators = GetTypeDiscriminators(baseType);
				if (discriminators.TryGetValue(symbol, out var discriminator)) {
					list.Add((baseType, discriminator));
				}
			}
			var array = list.Count == 0 ? Array.Empty<(ITypeSymbol, string)>() : list.ToArray();
			cache[symbol] = array;
			return array;
		}

		IReadOnlyDictionary<ITypeSymbol, string> GetTypeDiscriminators(ITypeSymbol type) {
			return indexes.GetOrAdd(type, StoreTypeDiscriminator);
		}

		IReadOnlyDictionary<ITypeSymbol, string> StoreTypeDiscriminator(ITypeSymbol type) {
			Dictionary<ITypeSymbol, string> dict = new Dictionary<ITypeSymbol, string>(SymbolEqualityComparer.Default);
			foreach (var attribute in type.GetAttributes()) {
				if (attribute.AttributeClass.Is(compilation.JsonDerivedTypeAttribute())) {
					var derivedType = attribute.ConstructorArguments[0].Value as ITypeSymbol;
					if (derivedType != null) {
						string discriminator;
						if (attribute.ConstructorArguments.Length == 1) {
							discriminator = derivedType.Name;
						} else {
							discriminator = attribute.ConstructorArguments[1].Value?.ToString() ?? derivedType.Name;
						}
						dict[derivedType] = discriminator;
					}
				}
			}
			return dict;
		}
	}
}