namespace Albatross.CodeGen.Syntax {
	/// <summary>
	/// interface for any modifier (public, private, static, readonly etc.)
	/// </summary>
	public interface IModifier {
		public string Name { get; }
	}
}