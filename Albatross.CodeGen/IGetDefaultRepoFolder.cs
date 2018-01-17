using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	//retrieve the default repo folder for settingsm, composite generators and scenarios
	public interface IGetDefaultRepoFolder {
		string Get();
	}
}
