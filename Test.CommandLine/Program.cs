namespace Test.CommandLine {
	internal class Program {
		static Task<int> Main(string[] args) {
			return new MySetup().AddCommands().Parse(args).RegisterServices().Build().InvokeAsync();
		}
	}
}