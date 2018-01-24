using System;

namespace Albatross.CodeGen {
	public class OptionType {
		public Type ObjectType { get; set; }
		public string Description { get; set; }
		public string Title { get { return $"{ObjectType.Name} - {Description}"; } }
	}
}