using System.IO;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac.Core;
using Autofac;
using Albatross.CodeGen.Autofac;
using System.Linq;

namespace Albatross.CodeGen.ClassLoader {
	class Program {

		public class Options {
			[Option('a', "assembly-paths", Required = false, HelpText = "Comma delimited referenced assembly paths")]
			public string AssemblyPath { get; set; }

			[Option('t', "target", Required = true, HelpText = "Target assembly file")]
			public string TargetAssembly { get; set; }

			[Option('p', "pattern", Required = false, HelpText = "Regular expression pattern")]
			public string Pattern { get; set; }

			[Option('n', "namespace", Required = false, HelpText = "Specify the namespaces of the target types")]
			public IEnumerable<string> Namespaces { get; set; }

			[Option('c', "converter", Required = false, Default = nameof(Albatross.CodeGen.CSharp.Conversion.ConvertTypeToCSharpClass), HelpText = "Specify the converter used to convert types, by default it is the ConvertTypeToCSharpClass")]
			public string Converter { get; set; }
		}

		static int Main(string[] args) {
			Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(option => {
				try {
					ContainerBuilder builder = new ContainerBuilder();
					new Pack().Register(builder);
					IContainer container = builder.Build();
					HashSet<string> paths = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
					paths.Add(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
					if (!string.IsNullOrWhiteSpace(option.AssemblyPath)) {
						var items = (from item in (option.AssemblyPath ?? string.Empty).Split(',') select item.Trim()).ToArray();
						foreach(string item in items) {
							paths.Add(item);
						}
					}
					if (File.Exists(option.TargetAssembly)) {
						Assembly assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(option.TargetAssembly);
						var handle = new GetAssemblyTypes(container, paths);
						handle.Get(assembly, option.Pattern, option.Namespaces, option.Converter);
					} else {
						Console.Error.WriteLine($"Assembly file {option.TargetAssembly} doesn't exist");
					}
				} catch (Exception err) {
					Console.Error.WriteLine(err);
				}
			});
			return 0;
		}
	}
}
