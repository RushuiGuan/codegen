using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	internal class Program {
		static Task<int> Main(string[] args)
			=> new MySetup().AddCommands().Parse(args).RegisterServices().Build().InvokeAsync();
	}
}