using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	public static class Extension {
		public static string RemoveCarriageReturn(this string text) {
			return text.Replace("\r", "");
		}
	}
}