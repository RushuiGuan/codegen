using Albatross.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CompositeCodeGenrator<T> : ICodeGenerator<T> {
		IEnumerable<string> _generators;

		public CompositeCodeGenrator(string name, string cateogry, string description, string target, IEnumerable<string> generators) {
			_generators = generators;
			Name = name;
			Category = cateogry;
			Description = description;
		}

		public string Name { get; private set; }
		public string Category { get; private set; }
		public string Description { get; private set; }
		public string Seperator { get; set; }
		public string Target { get; private set; }

		public StringBuilder Build(StringBuilder sb, T t, ICodeGeneratorFactory factory) {
			foreach (var item in _generators) {
				var gen = factory.Get<T>(item);
				gen.Build(sb, t, factory);
				if (item != _generators.Last()) {
					sb.Append(Seperator);
				}
			}
			return sb;
		}
		public StringBuilder Build(StringBuilder sb, object t, ICodeGeneratorFactory factory) {
			return Build(sb, (T)t, factory);
		}
	}
}
