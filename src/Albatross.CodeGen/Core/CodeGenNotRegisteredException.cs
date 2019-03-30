using System;

namespace Albatross.CodeGen.Core {

	public class CodeGenNotRegisteredException : Exception {
		public CodeGenNotRegisteredException(string name):base($"Code Generator {name} is not registered") {
		}
	}
}