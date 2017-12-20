using Albatross.CodeGen;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CfgControlledCodeGeneratorFactory : ICodeGeneratorFactory {
		Dictionary<string, ICodeGenerator> _registration = new Dictionary<string, ICodeGenerator>();

		public CfgControlledCodeGeneratorFactory(CodeGeneratorLocations locations) {
			foreach (var item in locations) {
				LoadAssembly(item);
			}
		}

		void LoadAssembly(string location) {
			try {
				Assembly asm = Assembly.LoadFile(location);
					foreach (Type type in asm.GetTypes()) {
						if (typeof(ICodeGenerator).IsAssignableFrom(type) && type.GetCustomAttribute<CodeGeneratorAttribute>() != null) {
							var item = Lifestyle.Singleton.CreateRegistration(type, c);
							list.Add(item);
						}
					}
					return list;


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
			} catch (Exception err) {
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
