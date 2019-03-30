using System.IO;
using CommandLine;
using System;
using System.Collections.Generic;
using SimpleInjector;
using Albatross.CodeGen.SimpleInjector;
using System.Reflection;

namespace Albatross.CodeGen.ClassLoader
{
    class Program {

        public class Options {
            [Option('f', "file", Required = true, HelpText = "Input assembly file")]
            public string AssemblyFile { get; set; }

            [Option('p', "pattern", Required = false, HelpText = "Regular expression pattern")]
            public string Pattern{ get; set; }

            [Option('n', "namespace", Required = false, HelpText = "Specify the namespaces of the target types")]
            public IEnumerable<string> Namespaces{ get; set; }

            [Option('c', "converter", Required = false, Default =Constant.CSharpConverter, HelpText = "Specify the converter used to convert types, by default it is the ConvertTypeToCSharpClass")]
            public string Converter{ get; set; }
        }


        static int Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(option => {
                try
                {
                    Container container = new Container();
                    new Pack().RegisterServices(container);
                    if (File.Exists(option.AssemblyFile))
                    {
                        Assembly assembly = Assembly.LoadFile(option.AssemblyFile);
                        var handle = container.GetInstance<GetAssemblyTypes>();
                        handle.Get(assembly, option.Pattern, option.Namespaces, option.Converter);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Assembly file {option.AssemblyFile} doesn't exist");
                    }
                }
                catch (Exception err) {
                    Console.Error.WriteLine(err);
                }
            });
            return 0;
        }
    }
}
