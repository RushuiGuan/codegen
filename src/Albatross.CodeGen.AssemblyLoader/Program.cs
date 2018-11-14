using System.IO;
using System;
using System.Collections.Generic;
using CommandLine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Albatross.CodeGen.CSharp.Core;

namespace Albatross.CodeGen.AssemblyLoader {
    class Options {

        [Option(Required = true, HelpText = "Conversion Class Name")]
        public string ConversionClass { get; set; }

        [Option(Required = true)]
        public string SourceType { get; set; }

		[Option(Required = true)]
        public string Output { get; set; }

    }

    class Program {
        static void Main(string[] args) {
            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts));
        }

        static void HandleParseError(IEnumerable<Error> errs) {
		}

        static void RunOptionsAndReturnExitCode(Options opts) {
            Type sourceType = Type.GetType(opts.SourceType);
            Type conversionClass = Type.GetType(opts.ConversionClass);
			IConvertClass handle = (IConvertClass) Activator.CreateInstance(conversionClass);
            object obj = handle.Convert(sourceType);
            using (var writer = new JsonTextWriter(new StreamWriter(opts.Output))) {
                writer.WriteValue(obj);
            }
		}
    }
}
