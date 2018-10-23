using System;
using System.Collections.Generic;

namespace Albatross.CodeGen {
	//when controlled by cfg, types will be added at run time.  Container registration cannot be used, but contaner is still
	//needed to create a new type instance.  ObjectFactory is a wrapper on the container.  This is a ServiceLocator Pattern in disguise.
	//edge case only.  do not copy this pattern!
	public class CodeGeneratorFactory : IConfigurableCodeGenFactory {
		IObjectFactory factory;
		Dictionary<string, CodeGenerator> registration = new Dictionary<string, CodeGenerator>(StringComparer.InvariantCultureIgnoreCase);
		object sync = new object();
		public IEnumerable<CodeGenerator> Registrations => registration.Values;

		public CodeGeneratorFactory(IObjectFactory factory) {
			this.factory = factory;
		}

		public void Clear() {
			lock (sync) {
				registration.Clear();
			}
		}

		public void Register(CodeGenerator gen) {
			lock (sync) {
				registration[gen.Name] = gen;
			}
		}

		public ICodeGenerator Create(string name) {
			CodeGenerator codeGenerator = Get(name);
			ICodeGenerator handle = (ICodeGenerator)factory.Create(codeGenerator.GeneratorType);
			handle.Configure(codeGenerator.Data);
			return handle;
		}

		public CodeGenerator Get(string name) {
			if (registration.TryGetValue(name, out CodeGenerator codeGenerator)) {
				return codeGenerator;
			} else {
				throw new CodeGenNotRegisteredException(name);
			}
		}
	}
}