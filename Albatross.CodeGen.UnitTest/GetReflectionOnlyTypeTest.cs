using Albatross.CodeGen.CSharp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class GetReflectionOnlyTypeTest {

		[TestCase(@"C:\Workspace\GitHub\CodeGen\Albatross.CodeGen.UnitTest\", "Albatross.CodeGen.UnitTest.DeclareStatementTest", @"bin\debug\Albatross.CodeGen.UnitTest.dll")]
		[TestCase(@"C:\temp\work\ConsoleApp1", "ConsoleApp1.Test", @"bin\debug\ConsoleApp1.exe")]
		public void Test(string currentDirectory, string className, string assemblyLocation) {
			System.Environment.CurrentDirectory = currentDirectory;
			ObjectType objectType = new ObjectType {
				ClassName = className,
				AssemblyLocation = assemblyLocation,
			};
			Type type = new GetReflectionOnlyType().Get(objectType);
			Assert.NotNull(type);
			foreach (var method in type.GetMethods()) {
				method.ReturnType.ToString();
			}
		}
	}
}
