using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Faults {
    public class InvalidSourceTypeException : Exception    {
		public InvalidSourceTypeException() { }
		public InvalidSourceTypeException(Type registered, Type requested) : base($"Invalid code generator source type; registered:{registered?.FullName}; received:{requested?.FullName}") {
		}
    }
}
