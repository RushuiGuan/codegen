using Albatross.CodeGen.Shell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public interface IWorkspaceService {
		void Create<T>(Action<T> action = null, object id = null) where T:WorkspaceViewModel;
		void CloseWorkspace(WorkspaceViewModel vm);
		void Alert(string msg, string caption);
		bool Confirm(string msg, string caption);
	}
}
