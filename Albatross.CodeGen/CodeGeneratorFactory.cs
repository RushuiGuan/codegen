using Albatross.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CodeGeneratorFactory : ICodeGeneratorFactory {
		Dictionary<string, ICodeGenerator> _registration = new Dictionary<string, ICodeGenerator>();

		public CodeGeneratorFactory(IEnumerable<ICodeGenerator> items) {
			foreach (var item in items) {
				Type type = item.GetType();
				Type paramType = null;

				foreach (var interfaceType in type.GetInterfaces()) {
					if (interfaceType.IsGenericType && typeof(ICodeGenerator<>) == interfaceType.GetGenericTypeDefinition()) {
						paramType = interfaceType.GetGenericArguments()?.FirstOrDefault();
						break;
					}
				}
				string key;
				if (paramType == null) {
					key = item.Name;
				} else {
					key = $"{paramType.FullName}.{item.Name}";
				}
				_registration.Add(key, item);
			}
		}

		public ICodeGenerator<T> Get<T>(string name) {
			string key = typeof(T).GetGeneratorName(name);
			ICodeGenerator codeGenerator;
			if (_registration.TryGetValue(key, out codeGenerator)) {
				return (ICodeGenerator<T>) codeGenerator;
			} else {
				throw new CodeGenNotRegisteredException(typeof(T), name);
			}
		}

		public ICodeGenerator Get(Type type, string name) {
			string key = type.GetGeneratorName(name);
			ICodeGenerator codeGenerator;
			if (_registration.TryGetValue(key, out codeGenerator)) {
				return codeGenerator;
			} else {
				throw new CodeGenNotRegisteredException(type, name);
			}
		}
	}
}
