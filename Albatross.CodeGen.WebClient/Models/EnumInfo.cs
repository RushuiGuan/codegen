using Albatross.CodeAnalysis.Symbols;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.Models {
	public record class EnumInfo {
		public EnumInfo(Compilation compilation, INamedTypeSymbol symbol) {
			this.Name = symbol.Name;
			this.UseTextAsValue = false;
			if (symbol.TryGetAttribute(compilation.JsonConverterAttribute(), out var attributeData)) {
				if ((attributeData?.ConstructorArguments.FirstOrDefault().Value as ITypeSymbol)?.GetFullName() == My.JsonStringEnumConverter_ClassName) {
					this.UseTextAsValue = true;
				}
			}
			this.Members = symbol.GetMembers().OfType<IFieldSymbol>().Select(x => new EnumMember(x)).ToArray();
		}
		public string Name { get; }
		public IEnumerable<EnumMember> Members { get; }
		public bool UseTextAsValue { get; }
	}
}