namespace Albatross.CodeGen.Core {
	public interface IGetCustomCode {
		bool TryGet(string name, out string text);
	}
}
