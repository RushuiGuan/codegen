using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Faults {
    public class InvalidOptionTypeException : Exception
    {
		public InvalidOptionTypeException() { }
		public InvalidOptionTypeException(Type registered, Type requested) : base($"Invalid code generator option type; registered:{registered.FullName}; received:{requested.FullName}") {
		}
	}
}
