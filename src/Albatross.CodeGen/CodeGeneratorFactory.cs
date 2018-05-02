using Albatross.CodeGen.Core;
using Albatross.CodeGen.Faults;
using System;
using System.Collections.Generic;

namespace Albatross.CodeGen {
	//when controlled by cfg, types will be added at run time.  Container registration cannot be used, but contaner is still
	//needed to create a new type instance.  ObjectFactory is a wrapper on the container.  This is a ServiceLocator Pattern in disguise.
	//edge case only.  do not copy this pattern!
	public class CodeGeneratorFactory : IConfigurableCodeGenFactory {
		IObjectFactory factory;
		Dictionary<string, CodeGenerator> _registration = new Dictionary<string, CodeGenerator>(StringComparer.InvariantCultureIgnoreCase);
		object _sync = new object();
		public IEnumerable<CodeGenerator> Registrations => _registration.Values;

		public CodeGeneratorFactory(IObjectFactory factory) {
			this.factory = factory;
		}

		public void Clear() {
			lock (_sync) {
				_registration.Clear();
			}
		}

		public void Register(CodeGenerator gen) {
			lock (_sync) {
				_registration[gen.Name] = gen;
			}
		}

		public ICodeGenerator<T, O> Create<T, O>(string name) where T:class where O:class {
			CodeGenerator codeGenerator = Get(typeof(T), name);
			var handle = (ICodeGenerator<T, O>)factory.Create(codeGenerator.GeneratorType);
			handle.Configure(codeGenerator.Data);
			return handle;
		}

		public object Create(Type type, string name) {
			CodeGenerator codeGenerator = Get(type, name);
			var handle = factory.Create(codeGenerator.GeneratorType);
			handle.GetType().GetMethod(nameof(ICodeGenerator<object, object>.Configure)).Invoke(handle, new[] { codeGenerator.Data });
			return handle;
		}

		public CodeGenerator Get(Type type, string name) {
			if (_registration.TryGetValue(name, out CodeGenerator codeGenerator)) {
				if (!codeGenerator.SourceType.IsAssignableFrom(type)) {
					throw new InvalidSourceTypeException(codeGenerator.SourceType, type, codeGenerator.Name);
				}
				return codeGenerator;
			} else {
				throw new CodeGenNotRegisteredException(name);
			}
		}
	}
}
