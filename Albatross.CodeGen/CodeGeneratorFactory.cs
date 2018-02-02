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
		Dictionary<string, CodeGenerator> _registration = new Dictionary<string, CodeGenerator>();
		IFactory<IEnumerable<Assembly>> assemblyFactory;
		IFactory<IEnumerable<IComposite>> compositeFactory;
		object _sync = new object();

		public IEnumerable<CodeGenerator> Registrations => _registration.Values;

		public CodeGeneratorFactory(IFactory<IEnumerable<Assembly>> assemblyFactory, IFactory<IEnumerable<IComposite>> compositeFactory, IObjectFactory factory) {
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
				foreach (var item in items) {
					this.Register(item);
				}
			}
		}

		public void Register(IComposite item) {
			lock (_sync) {
				Type type = typeof(CompositeCodeGenerator<,>);
				var gen = new CodeGenerator {
					Name = item.Name,
					Target = item.Target,
					Category = item.Category,
					Description = item.Description,
					GeneratorType = type.MakeGenericType(item.SourceType, item.OptionType),
					SourceType = item.SourceType,
					OptionType = item.OptionType,
				};
				_registration[gen.Key] = gen;
			}
		}
		public void Register(Assembly asm) {
			lock (_sync) {
				foreach (Type type in asm.GetTypes()) {
					Type interfaceType = type.FindInterfaces(new TypeFilter((t, obj) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICodeGenerator<,>)), null).FirstOrDefault();
					if (interfaceType != null) {
						CodeGeneratorAttribute attrib = type.GetCustomAttribute<CodeGeneratorAttribute>();
						if (attrib != null) {
							Type[] arguments = interfaceType.GetGenericArguments();
							CodeGenerator gen = new CodeGenerator {
								Name = attrib.Name,
								Target = attrib.Target,
								Category = attrib.Category,
								Description = attrib.Description,
								GeneratorType = type,
								SourceType = arguments[0],
								OptionType = arguments[1],
							};
							_registration[gen.Key] = gen;
						}
					}
				}
			}
		}

		public ICodeGenerator<T, O> Get<T,O>(string name) {
			string key = typeof(T).GetGeneratorKey(name);
			if (_registration.TryGetValue(key, out CodeGenerator codeGenerator)) {
				return (ICodeGenerator<T, O>)factory.Create(codeGenerator.GeneratorType);
			} else {
				throw new CodeGenNotRegisteredException(typeof(T), name);
			}
		}

		public object Get(Type srcType, string name) {
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
