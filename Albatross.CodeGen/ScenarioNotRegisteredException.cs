using System;
using System.Runtime.Serialization;

namespace Albatross.CodeGen {
	public  class ScenarioNotRegisteredException : Exception {
		public ScenarioNotRegisteredException(string name) : base($"Scenario {name} is not registered") {
		}
	}
}