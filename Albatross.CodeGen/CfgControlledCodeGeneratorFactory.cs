using Albatross.CodeGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen{
	//when controlled by cfg, types will be added at run time.  Container registration cannot be used, but contaner is still
	//needed to create a new type instance.  ObjectFactory is a wrapper on the container.  This is a ServiceLocator Pattern in disguise.
	//edge case only.  do not copy this pattern!
	public class CfgControlledCodeGeneratorFactory : IConfigurableCodeGenFactory {
		IObjectFactory factory;
		Dictionary<string, ICodeGenerator> _registration = new Dictionary<string, ICodeGenerator>();
		IFactory<IEnumerable<Assembly>> assemblyFactory;
		IFactory<IEnumerable<Composite>> compositeFactory;
		object _sync = new object();

		public IEnumerable<ICodeGenerator> Registrations => _registration.Values;

		public CfgControlledCodeGeneratorFactory(IFactory<IEnumerable<Assembly>> assemblyFactory, IFactory<IEnumerable<Composite>> compositeFactory, IObjectFactory factory) {
			this.assemblyFactory = assemblyFactory;
			this.compositeFactory = compositeFactory;
			this.factory = factory;
			Register();
		}

		public void Clear() {
			lock (_sync) {
				_registration.Clear();
			}
		}

		public void Register() {
			Clear();

			var list = assemblyFactory.Get();
			foreach (var item in list) {
				Register(item);
			}
			var items = compositeFactory.Get();
			if (items != null) {
				this.Register(items);
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
			lock (_sync) {
				foreach (Type type in asm.GetTypes()) {
					if (typeof(ICodeGenerator).IsAssignableFrom(type) && type.GetCustomAttribute<CodeGeneratorAttribute>() != null) {
						ICodeGenerator c = (ICodeGenerator)factory.Create(type);
						string key = c.GetName();
						_registration[key] = c;
					}
				}
			}
		}

		public ICodeGenerator<T, O> Get<T,O>(string name) {
			string key = typeof(T).GetGeneratorName(name);
			if (_registration.TryGetValue(key, out ICodeGenerator codeGenerator)) {
				return (ICodeGenerator<T, O>)codeGenerator;
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
