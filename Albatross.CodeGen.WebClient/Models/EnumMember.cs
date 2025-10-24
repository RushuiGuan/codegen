using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Models {
	public record class EnumMember {
		public EnumMember(IFieldSymbol symbol) {
			this.Name = symbol.Name;
			this.NumericValue = System.Convert.ToInt64(symbol.ConstantValue);
		}
		public string Name { get; }
		public long NumericValue { get; }
	}
}