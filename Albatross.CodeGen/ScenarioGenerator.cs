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

		public Type SourceType => typeof(Scenario);

		public StringBuilder Build(StringBuilder sb, Scenario t, object options, ICodeGeneratorFactory factory) {
			foreach (Step step in t.Steps) {
				if (step.SourceType == typeof(Scenario)) {
					this.Build(sb, (Scenario)step.Source, options, factory);
				} else {
					ICodeGenerator gen = factory.Get(step.SourceType, step.Generator);
					gen.Build(sb, step.Source, step.Options, factory);
				}
			}
			return sb;
		}

		public StringBuilder Build(StringBuilder sb, object t, object options, ICodeGeneratorFactory factory) {
			return Build(sb, t, options, factory);
		}
	}
}
