using System;

namespace Albatross.CodeGen.Core {
	public class OptionType {
		public Type ObjectType { get; set; }
		public string Description { get; set; }
		public string Title { get { return $"{ObjectType.Name} - {Description}"; } }

		public override int GetHashCode() {
			if (ObjectType == null) {
				return 0;
			} else {
				return ObjectType.GetHashCode();
			}
		}

		public override bool Equals(object obj) {
			return ObjectType == ((OptionType)obj)?.ObjectType;
		}
	}
}