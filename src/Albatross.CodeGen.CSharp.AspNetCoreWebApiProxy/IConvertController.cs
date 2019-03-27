using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.AspNetCoreWebApiProxy {
	/// <summary>
	/// Convert a type of AspNet Controller to the ControllerClass
	/// </summary>
	public interface IConvertController {
		ControllerClass Convert(Type type);
	}
}
