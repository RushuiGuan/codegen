using Albatross.CodeGen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class ScenarioGenerator : ICodeGenerator<Scenario> {
		public string Name => "Scenario Generator";
		public string Category => "System";
		public string Description => null;
		public string Target => null;

		public StringBuilder Build(StringBuilder sb, Scenario t, ICodeGeneratorFactory factory) {
			foreach (Step step in t.Steps) {
				Type type = Type.GetType(step.SourceType);
				object obj = JsonConvert.DeserializeObject(step.Source, type);
				if (type == typeof(Scenario)) {
					this.Build(sb, (Scenario)obj, factory);
				} else {
					string key = type.GetGeneratorName(step.Generator);
				}
			}
			return sb;
		}

		public StringBuilder Build(StringBuilder sb, object t, ICodeGeneratorFactory factory) {
			return Build(sb, t, factory);
		}
	}
}
