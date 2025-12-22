using System;

namespace Albatross.CodeGen {
	/// <summary>
	/// Exception thrown when general code generation operations fail
	/// </summary>
	public class CodeGenException : Exception {
		/// <summary>
		/// Initializes a new instance of the CodeGenException class with a specified error message
		/// </summary>
		/// <param name="msg">The message that describes the error</param>
		public CodeGenException(string msg) : base(msg) { }
	}
}