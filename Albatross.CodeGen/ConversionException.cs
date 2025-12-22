using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	/// <summary>
	/// Exception thrown when object conversion operations fail during code generation
	/// </summary>
	public class ConversionException : Exception {
		/// <summary>
		/// Initializes a new instance of the ConversionException class with a specified error message
		/// </summary>
		/// <param name="msg">The message that describes the error</param>
		public ConversionException(string msg) : base(msg) { }
		
		/// <summary>
		/// Initializes a new instance of the ConversionException class with default values
		/// </summary>
		public ConversionException() { }
	}
}