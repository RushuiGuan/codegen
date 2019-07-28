

using System.IO;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using Albatross.CodeGen.Core;
using Newtonsoft.Json;

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
					ServiceCollection services = new ServiceCollection();
					services.AddDefaultCodeGen();
					HashSet<string> paths = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
					paths.Add(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
					if (!string.IsNullOrWhiteSpace(option.AssemblyPath)) {
						var items = (from item in (option.AssemblyPath ?? string.Empty).Split(',') select item.Trim()).ToArray();
						foreach (string item in items) {
							paths.Add(item);
						}
					}
					var getAssemblyTypes = new GetAssemblyTypes(paths);
					Type converterType = getAssemblyTypes.LoadTypeByName(option.Converter);
					if (!services.TryAddConverter(converterType)) {
						throw new Exception($"Error loading converter type: ${converterType}");
					}
					var provider = services.BuildServiceProvider();


					Regex regex = null;
					IConvertObject<Type> converter = provider.GetRequiredService(converterType) as IConvertObject<Type>;
					if (!string.IsNullOrEmpty(option.Pattern)) {
						regex = new Regex(option.Pattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
					}

					List<CSharp.Model.Class> list = new List<CSharp.Model.Class>();
					var writer = new JsonTextWriter(Console.Out);
					JsonSerializer serializer = new JsonSerializer();
					writer.Formatting = Formatting.None;
					writer.WriteStartArray();
					getAssemblyTypes.LoadAssemblyTypes(option.TargetAssembly, type => {
						if (!type.IsAnonymousType() && !type.IsInterface && type.IsPublic) {
							bool match = (option.Namespaces?.Count() ?? 0) == 0 || option.Namespaces.Contains(type.Namespace, StringComparer.InvariantCultureIgnoreCase);
							if (match) {
								match = match && (regex == null || regex.IsMatch(type.FullName));
							}
							if (match) {
								var data = converter.Convert(type);
								serializer.Serialize(writer, data);
							}
						}
					});
					writer.WriteEndArray();
					writer.Flush();
				} catch (Exception err) {
					Console.Error.WriteLine(err);
				}
			});
			return 0;
		}
	}
}
