using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Core;
using Albatross.CodeGen.SimpleInjector;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(GetMethodSignature))]
	public class TestGetMethodSignature : TestBase{
		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}


		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(new Method("test"){
					Parameters = new Parameter[]{
						new Parameter("a") {
							Type = DotNetType.Integer
						},
						new Parameter("b") {
							Type = DotNetType.String
						},
						new Parameter("c") {
							Type = DotNetType.Object
						}
					}
				}) {
					ExpectedResult = "test(int a,string b,object c)",
				}
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Method method) {
			var handle = Get<GetMethodSignature>();
			return handle.Get(method);
		}
	}
}
