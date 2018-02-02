using Albatross.CodeGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen{
	public class StaticCodeGenFactory : ICodeGeneratorFactory {
		IObjectFactory factory;
		Dictionary<string, CodeGenerator> _registration = new Dictionary<string, CodeGenerator>();

		public IEnumerable<CodeGenerator> Registrations => _registration.Values;

		public StaticCodeGenFactory(IObjectFactory factory, IEnumerable<CodeGenerator> registration) {
			this.factory = factory;
			foreach (var item in registration) {
				_registration.Add(item.SourceType.GetGeneratorKey(item.Name), item);
			}
		}

		public ICodeGenerator<T, O> Create<T,O>(string name) {
			string key = typeof(T).GetGeneratorKey(name);
			if (_registration.TryGetValue(key, out CodeGenerator codeGenerator)) {
				return (ICodeGenerator<T, O>)factory.Create(codeGenerator.GeneratorType);
			} else {
				throw new CodeGenNotRegisteredException(typeof(T), name);
			}
		}

		public object Create(Type srcType, string name) {
			string key = srcType.GetGeneratorKey(name);
			if (_registration.TryGetValue(key, out CodeGenerator codeGenerator)) {
				return factory.Create(codeGenerator.GeneratorType);
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
