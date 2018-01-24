using Albatross.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class ContainerControlledCodeGenFactory : ICodeGeneratorFactory {
		Dictionary<string, ICodeGenerator> _registration = new Dictionary<string, ICodeGenerator>();

		public ContainerControlledCodeGenFactory(IEnumerable<ICodeGenerator> items) {
			foreach (var item in items) {
				string key = item.GetName();
				_registration.Add(key, item);
			}
		}

		public IEnumerable<ICodeGenerator> Registrations => _registration.Values;

		public ICodeGenerator<T, O> Get<T, O>(string name) {
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
