using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Faults {
    public class InvalidSourceTypeException : Exception    {
		public InvalidSourceTypeException() { }
		public InvalidSourceTypeException(Type registered, Type requested, string name) : base($"Code Generator \"{name}\" has a registered source type of {registered.FullName} but received {requested.FullName} instead") {
		}
    }
}
