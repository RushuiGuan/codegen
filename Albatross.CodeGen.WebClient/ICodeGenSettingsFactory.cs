using Albatross.CodeGen.WebClient.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.WebClient {
	public interface ICodeGenSettingsFactory {
		T Get<T>() where T : CodeGenSettings, new ();
	}
}
