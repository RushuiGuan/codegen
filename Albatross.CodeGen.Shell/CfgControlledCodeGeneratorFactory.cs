using Albatross.CodeGen;
using Albatross.Logging.Core;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	//when controlled by cfg, types will be added at run time.  Container registration cannot be used, but contaner is still
	//needed to create a new type instance.  ObjectFactory is a wrapper on the container.  This is a ServiceLocator Pattern in disguise.
	//edge case only.  do not copy this pattern!
	public class CfgControlledCodeGeneratorFactory : IConfigurableCodeGenFactory {
		Dictionary<string, ICodeGenerator> _registration = new Dictionary<string, ICodeGenerator>();
		IObjectFactory _factory;
		ILog _log;
		AssemblyLocationRepository _settingRepository;
		CompositeRepository _compositeRepository;
		object _sync = new object();

		public IEnumerable<ICodeGenerator> Registrations => _registration.Values;

		public CfgControlledCodeGeneratorFactory(AssemblyLocationRepository settingRepository, CompositeRepository compositRepo, IObjectFactory factory, ILogFactory logFactory) {
			_settingRepository = settingRepository;
			_compositeRepository = compositRepo;
			_factory = factory;
			_log = logFactory.Get(this);
		}

		public void Clear() {
			lock (_sync) {
				_registration.Clear();
			}
		}

		public void Register() {
			lock (_sync) {
				var list = _settingRepository.GetAssembly();
				foreach (var item in list) {
					Register(item);
				}
				var items = _compositeRepository.Get();
				if (items != null) {
					this.Register(items);
				}
			}
		}

		public void Register(IEnumerable<Composite> items) {
			lock (_sync) {
				foreach (var item in items) {
					var gen = new CompositeCodeGenerator(item);
					_registration[gen.GetName()] = gen;
				}
			}
		}
		public void Register(Assembly asm) {
			foreach (Type type in asm.GetTypes()) {
				if (typeof(ICodeGenerator).IsAssignableFrom(type) && type.GetCustomAttribute<CodeGeneratorAttribute>() != null) {
					ICodeGenerator c = (ICodeGenerator)_factory.Create(type);
					string key = c.GetName();
					_registration[key] = c;
				}
			}
		}

		public ICodeGenerator<T> Get<T>(string name) {
			string key = typeof(T).GetGeneratorName(name);
			if (_registration.TryGetValue(key, out ICodeGenerator codeGenerator)) {
				return (ICodeGenerator<T>)codeGenerator;
			} else {
				throw new CodeGenNotRegisteredException(typeof(T), name);
			}
		}

		public ICodeGenerator Get(Type type, string name) {
			string key = type.GetGeneratorName(name);
			if (_registration.TryGetValue(key, out ICodeGenerator codeGenerator)) {
				return codeGenerator;
			} else {
				throw new CodeGenNotRegisteredException(type, name);
			}
		}
	}
}
