using Albatross.CodeGen.Database;
using Albatross.CodeGen.Shell.View;
using Albatross.CodeGen.Shell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public class ViewLocator {
		public Type GetViewType(Type viewModelType) {
			if (viewModelType == typeof(CodeGeneratorCollectionViewModel)) {
				return typeof(CodeGeneratorCollectionView);
			} else if (viewModelType == typeof(CompositeDetailViewModel)) {
				return typeof(CompositeDetailView);
			} else if (viewModelType == typeof(Table)) {
				return typeof(TableInputView);
			} else if (viewModelType == typeof(StoredProcedure)) {
				return typeof(StoredProcedureInputView);
			} else if (viewModelType == typeof(Server)) {
				return typeof(ServerInputView);
			} else {
				return null;
			}
		}
	}
}
