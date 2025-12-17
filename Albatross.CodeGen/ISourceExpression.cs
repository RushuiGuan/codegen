namespace Albatross.CodeGen {
	/// <summary>
	/// an expression that's used to reference a source file or module
	/// </summary>
	public interface ISourceExpression : IExpression {
		string Source { get; }
	}
}