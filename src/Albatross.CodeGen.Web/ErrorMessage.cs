using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Web {
	public class ErrorMessage {
		public string Message { get; set; }
		public string ExceptionType { get; set; }
		public string StackTrace { get; set; }
		public string ExceptionMessage { get; set; }
		public int StatusCode { get; set; }
	}
}
