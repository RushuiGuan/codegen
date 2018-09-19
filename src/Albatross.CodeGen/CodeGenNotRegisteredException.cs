﻿using System;

namespace Albatross.CodeGen {

	public class CodeGenNotRegisteredException : Exception {
		public CodeGenNotRegisteredException(Type type, string name):base($"Code Generator {name} is not registered for type: {type.FullName}") {
		}
	}
}