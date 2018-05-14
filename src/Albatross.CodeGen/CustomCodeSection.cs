using System.IO;
using System.Collections.Generic;
using Albatross.CodeGen.Core;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Albatross.CodeGen {
	public abstract class CustomCodeSection : ICustomCodeSection {
		public abstract string ApplyTo { get; }
		public abstract string StartTagPattern { get; }
		public abstract string EndTagPattern { get; }

		public CustomCodeSection() {
			startTag = new Regex(StartTagPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			endTag = new Regex(EndTagPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
		}

		Regex startTag, endTag;

		Dictionary<string, string> data = new Dictionary<string, string>();

		public void Load(string code) {
			data.Clear();
			using (StringReader reader = new StringReader(code)) {
				Stack<Tuple<string, StringBuilder>> stack = new Stack<Tuple<string, StringBuilder>>();
				for(string line = reader.ReadLine(); line != null; line = reader.ReadLine()) {
					string tag;
					if (TryGetBeginTag(line, out tag)) {
						stack.Push(new Tuple<string, StringBuilder>(tag, new StringBuilder()));
					}else if(IsEndTag(line)) {
						if (stack.Count > 0) {
							var tuple = stack.Pop();
							data[tuple.Item1] = tuple.Item2.ToString();
						}
					} else {
						if (stack.Count > 0) {
							stack.Peek().Item2.AppendLine(line);
						}
					}
				}
			}
		}

		protected virtual bool TryGetBeginTag(string input, out string tag) {
			Match match = startTag.Match(input);
			tag = match.Groups[1].Value;
			return match.Success;
		}

		protected virtual bool IsEndTag(string input) {
			Match match = endTag.Match(input);
			return match.Success;
		}

		public abstract void Write(string name, int tabCount, StringBuilder sb);

		public string Read(string name) {
			string content = null;
			data.TryGetValue(name, out content);
			return content;
		}

		public void Clear() {
			data.Clear();
		}
	}
}
