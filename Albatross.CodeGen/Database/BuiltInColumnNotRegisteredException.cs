using System;
using System.Runtime.Serialization;

namespace Albatross.CodeGen.Database{
	public class BuiltInColumnNotRegisteredException : Exception {
		public BuiltInColumnNotRegisteredException(string name) : base($"BuiltInColumn {name} is not registered!") {
		}
	}
}