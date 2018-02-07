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
	public class CodeGeneratorFactory : IConfigurableCodeGenFactory {
		IObjectFactory factory;
		Dictionary<string, CodeGenerator> _registration = new Dictionary<string, CodeGenerator>(StringComparer.InvariantCultureIgnoreCase);
		IFactory<IEnumerable<Assembly>> assemblyFactory;
		IFactory<IEnumerable<IComposite>> compositeFactory;
		object _sync = new object();

		public IEnumerable<CodeGenerator> Registrations => _registration.Values;

		public CodeGeneratorFactory(IFactory<IEnumerable<Assembly>> assemblyFactory, IFactory<IEnumerable<IComposite>> compositeFactory, IObjectFactory factory) {
			this.assemblyFactory = assemblyFactory;
			this.compositeFactory = compositeFactory;
			this.factory = factory;
			//Register();
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
				this.Register(item);
			}
			var items = compositeFactory.Get();
			if (items != null) {
				foreach (var item in items) {
					this.Register(item);
				}
			}
		}

		public void Register(CodeGenerator gen) {
			lock (_sync) {
				_registration[gen.Key] = gen;
			}
		}

		public ICodeGenerator<T, O> Create<T,O>(string name) {
			CodeGenerator codeGenerator;
			if (_registration.TryGetValue(typeof(T).GetGeneratorKey(name), out codeGenerator) || _registration.TryGetValue(typeof(object).GetGeneratorKey(name), out codeGenerator)) {
				var handle = (ICodeGenerator<T, O>)factory.Create(codeGenerator.GeneratorType);
				handle.Configure(codeGenerator.Data);
				return handle;
			} else {
				throw new CodeGenNotRegisteredException(typeof(T), name);
			}
		}

		public object Create(Type srcType, string name) {
			CodeGenerator codeGenerator;
			if (_registration.TryGetValue(srcType.GetGeneratorKey(name), out codeGenerator) || _registration.TryGetValue(typeof(object).GetGeneratorKey(name), out codeGenerator)) {
				var handle = factory.Create(codeGenerator.GeneratorType);
				handle.GetType().GetMethod(nameof(ICodeGenerator<int,int>.Configure)).Invoke(handle, new[] { codeGenerator.Data });
				return handle;
			} else {
				throw new CodeGenNotRegisteredException(srcType, name);
			}
		}

		public CodeGenerator GetMetadata(Type srcType, string name) {
			string key = srcType.GetGeneratorKey(name);
			if (_registration.TryGetValue(key, out CodeGenerator codeGenerator)) {
				return codeGenerator;
			} else {
				throw new CodeGenNotRegisteredException(srcType, name);
			}
		}
	}
}
