using Albatross.CodeAnalysis.Testing;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.UnitTest {
	public class TestCodeNodeAndFactory {
		private sealed record class TreeNode(string Name, params ICodeNode[] ChildrenNodes) : CodeNode {
			public override IEnumerable<ICodeNode> Children => ChildrenNodes;
			public override TextWriter Generate(TextWriter writer) {
				writer.Write(Name);
				return writer;
			}
		}

		[Fact]
		public void GetDescendants_ShouldTraverseDepthFirst() {
			var root = new TreeNode("root",
				new TreeNode("left",
					new TreeNode("left.leaf")),
				new TreeNode("right"));

			var descendants = root.GetDescendants().OfType<TreeNode>().Select(x => x.Name).ToArray();

			descendants.Should().Equal("left", "left.leaf", "right");
		}

		[Fact]
		public async Task CompilationFactory_ShouldReturnSameCompilationInstance() {
			var compilation = await "class C {}".CreateNet8CompilationAsync();
			var factory = new CompilationFactory(compilation);

			var result = factory.Get();

			ReferenceEquals(compilation, result).Should().BeTrue();
		}
	}
}
