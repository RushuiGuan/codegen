using Albatross.CodeGen.Database;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	/// <summary>
	/// Create a variable name that is camel cased, with spaces replaced by _ (underline)
	/// </summary>
	public class CreateSqlVariableName : ICreateSqlVariableName {
		enum LetterCasing {
			None,
			Lower,
			Upper,
		}

		public string Get(string name) {
			StringBuilder sb = new StringBuilder("@");
			LetterCasing next = LetterCasing.Lower;
			foreach (char c in name) {
				if (c == ' ') {
					if (next == LetterCasing.None) { next = LetterCasing.Upper; }
				} else if (char.IsLetter(c)) {
					if (next == LetterCasing.Lower) {
						sb.Append(char.ToLower(c));
					} else if (next == LetterCasing.Upper) {
						sb.Append(char.ToUpper(c));
					} else {
						sb.Append(c);
					}
					next = LetterCasing.None;
				} else if (char.IsNumber(c)) {
					sb.Append(c);
					next = LetterCasing.None;
				} else {
					sb.Append('_');
					next = LetterCasing.None;
				}
			}
			return sb.ToString();
		}
	}
}
