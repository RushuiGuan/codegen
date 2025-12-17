using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	internal class Program {
		static async Task<int> Main(string[] args) {
			var setup = new MySetup().AddCommands();
			var parser = setup.CommandBuilder.Build();
			return await parser.InvokeAsync(args);
		}
	}
}