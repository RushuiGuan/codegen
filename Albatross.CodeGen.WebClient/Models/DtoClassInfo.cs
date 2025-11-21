using Albatross.CodeAnalysis.Symbols;
using Albatross.Collections;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.Models {
	public record class DtoClassInfo {
		public DtoClassInfo(INamedTypeSymbol symbol, IJsonDerivedTypeIndex index) {
			this.Name = symbol.Name;
			this.FullName = symbol.GetFullName();
			var properties = new Dictionary<string, DtoClassPropertyInfo>();
			symbol.AllInterfaces.ForEach(x => BaseTypes.Add(x.GetFullName()));
			var derivedTypeDiscriminators = index.GetDerivedTypeDiscriminators(symbol);
			if (derivedTypeDiscriminators.Length > 0) {
				this.TypeDiscriminator = derivedTypeDiscriminators.First().Item2;
			}
			while (symbol != null) {
				foreach (var item in symbol.GetMembers().OfType<IPropertySymbol>()
					.Where(x => !(symbol.IsRecord && x.Name == "EqualityContract"))
					.Select(x => new DtoClassPropertyInfo(x))) {

					// [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
					if (item.Symbol.TryGetAttribute("System.Text.Json.Serialization.JsonIgnoreAttribute", out var attribute)) {
						if (!attribute!.TryGetNamedArgument("Condition", out var conditionValue) || System.Convert.ToInt32(conditionValue.Value) == (int)System.Text.Json.Serialization.JsonIgnoreCondition.Always) {
							continue;
						}
					}
					if (!properties.ContainsKey(item.Name)) {
						properties.Add(item.Name, item);
					}
				}
				if (symbol.BaseType != null && symbol.BaseType.SpecialType != SpecialType.System_Object) {
					BaseTypes.Add(symbol.BaseType.GetFullName());
					symbol = symbol.BaseType;
				} else {
					symbol = null;
				}
			}
			Properties = properties.Values.ToArray();
		}

		public string Name { get; }
		public string FullName { get; }
		public DtoClassPropertyInfo[] Properties { get; }
		public HashSet<string> BaseTypes { get; } = new HashSet<string>();
		/// <summary>
		/// Looking for the JsonDerivedTypeAttribute of a base class to get the type discriminator value
		/// the derivedType of the JsonDerivedTypeAttribute should be this class
		/// </summary>
		public string? TypeDiscriminator { get; set; }
	}
}