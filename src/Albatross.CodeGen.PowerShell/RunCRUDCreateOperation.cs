using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Start, "CRUDCreateOperation")]
	public class RunCRUDCreateOperation : PSCmdlet {
		[Parameter(Mandatory =true, ValueFromPipeline =true, Position =0)]
		public CRUDProjectOptions Options { get; set; }

		protected override void ProcessRecord() {
			Table table = Ioc.Get<IGetTable>().Get(new Albatross.Database.Database { DataSource = Options.Server, SSPI = true, InitialCatalog = Options.Database }, Options.Schema, Options.Name);
			GeneratePOCOClass(table);
		}


		void GeneratePOCOClass(Table table) {
			CSharpClassOption option = new CSharpClassOption {
				Name = table.Name.Proper(),
				Namespace = Options.ClassNamespace,
				Imports = new []{ "System" },
				PropertyTypeOverrides = Options.ClassPropertyTypeOverrides,
			};
			string filename = GetPath(Options.RootPath, Options.ClassPath, $"{Options.Name}.cs");
			Ioc.Get<ICustomCodeSection>().Load(ReadFile(filename));
			ICodeGenerator gen = Ioc.Get<ICodeGeneratorFactory>().Create("csharp.table.class");
			StringBuilder sb = new StringBuilder();
			gen.Generate(sb, table, option);
			WriteFile(filename, sb);
		}

		string ReadFile(string file) {
			if (File.Exists(file)) {
				using (StreamReader reader = new StreamReader(file)) {
					return reader.ReadToEnd();
				}
			} else {
				return null;
			}
		}
		void WriteFile(string file, StringBuilder sb) {
			using (StreamWriter writer = new StreamWriter(file)) {
				writer.Write(sb.ToString());
				writer.Flush();
				writer.BaseStream.SetLength(writer.BaseStream.Position);
			}
		}
		string GetPath(string basePath, string path, string filename) {
			basePath = basePath?.TrimEnd('\\', '/');
			path = path?.TrimEnd('\\', '/');

			if (Path.IsPathRooted(path)) {
				return path + "\\" + filename;
			} else if (string.IsNullOrEmpty(path)) {
				return basePath + "\\" + filename;
			} else {
				return basePath + "\\" + path + "\\" + filename;
			}
		}
	}
}
