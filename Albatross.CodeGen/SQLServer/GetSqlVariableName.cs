using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class GetSqlVariableName : IGetVariableName {
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
