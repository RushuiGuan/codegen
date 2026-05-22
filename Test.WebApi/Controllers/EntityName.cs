using System;

namespace Test.WebApi.Controllers {
	public class EntityName : IParsable<EntityName> {
		public string Value { get; }

		private EntityName(string value) {
			Value = value;
		}

		public static bool TryParse(string? value, IFormatProvider? provider, out EntityName tenantName) {
			tenantName = new EntityName(value!.ToLowerInvariant());
			return true;
		}

		public static EntityName Parse(string value, IFormatProvider? provider) {
			return new EntityName(value!.ToLowerInvariant());
		}
		public override string ToString() => Value ?? string.Empty;
	}
}