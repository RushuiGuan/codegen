using Albatross.CodeGen.CSharp.Core;
using System;

namespace Albatross.CodeGen.CSharp.AspNetCoreWebApiProxy {
	public class ControllerMethod : Method {
		public string HttpAction { get; set; }
		public string Route { get; set; }
	}
}
