using System;

namespace Albatross.CodeGen.Faults {
	public class CustomCodeSectionNotRegisteredException : Exception {
		public CustomCodeSectionNotRegisteredException(string name) : base($"Custom code section for target {name} is not registered") {
		}
	}
}