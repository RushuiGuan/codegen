using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace ConsoleApp1 {
	class Program {
		static void Main(string[] args) {
            const string path = "/Users/rushui/git/codegen/src/TestProjects/bin/ProjectB.dll";

            Run(path);
            Console.ReadLine();
            Run(path);
        }
        static void Run(string path) {
            Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
            //    Assembly asm = Assembly.LoadFile(path);

            foreach (Type type in asm.GetTypes()) {
                Console.WriteLine(type.FullName);
                foreach (PropertyInfo p in type.GetProperties()) {
                    Console.WriteLine(p.Name);
                }
            }
        }
        static void Run2() {
            Process p = new Process();
        }
	}

}
