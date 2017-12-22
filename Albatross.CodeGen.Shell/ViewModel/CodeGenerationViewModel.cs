using Albatross.Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class CodeGenerationViewModel : WorkspaceViewModel {
		ICodeGenerator codeGenerator;
		ICodeGeneratorFactory factory;
		ILog log;
		public CodeGenerationViewModel(IWorkspaceService svc, ICodeGeneratorFactory factory, ILogFactory logFactory) : base(svc, logFactory) {
			this.factory = factory;
			Title = "Code Generation";
			log = logFactory.Get(this);
		}

		object _source;
		public object Source {
			get { return _source; }
			set {
				_source = value;
				RaisePropertyChanged(nameof(Source));
			}
		}
		public void Init(ICodeGenerator codeGenerator) {
			Title = codeGenerator.Name;
			this.codeGenerator = codeGenerator;
			Source = Activator.CreateInstance(codeGenerator.SourceType);
		}
		public RelayCommand RunCommand {
			get {
				return new RelayCommand(args => Run());
			}
		}
		void Run() {
			try {
				Result = null;
				StringBuilder sb = new StringBuilder();
				codeGenerator.Build(sb, Source, null, factory);
				Result = sb.ToString();
			} catch (Exception err) {
				log.Error(err);
				WorkspaceService.Alert(err.Message, "Error");
			}
		}

		string _result;
		public string Result {
			get { return _result; }
			set {
				if (value != _result) {
					_result = value;
					RaisePropertyChanged(nameof(Result));
				}
			}
		}
	}
}
