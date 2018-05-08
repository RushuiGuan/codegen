﻿using System.IO;
using System.Collections.Generic;
using Albatross.CodeGen.Core;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Albatross.CodeGen {
	public abstract class ReadCustomCode : IReadCustomCode {
		public abstract string StartTagPattern { get; }
		public abstract string EndTagPattern { get; }

		public ReadCustomCode() {
			startTag = new Regex(StartTagPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			endTag = new Regex(EndTagPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
		}

		Regex startTag, endTag;

		public Dictionary<string, string> Read(string code) {
			Dictionary<string, string> dict = new Dictionary<string, string>();
			using (StringReader reader = new StringReader(code)) {
				Stack<Tuple<string, StringBuilder>> stack = new Stack<Tuple<string, StringBuilder>>();
				for(string line = reader.ReadLine(); line != null; line = reader.ReadLine()) {
					string tag;
					if (TryGetBeginTag(line, out tag)) {
						stack.Push(new Tuple<string, StringBuilder>(tag, new StringBuilder()));
					}else if(IsEndTag(line)) {
						if (stack.Count > 0) {
							var tuple = stack.Pop();
							dict[tuple.Item1] = tuple.Item2.ToString();
						}
					} else {
						if (stack.Count > 0) {
							stack.Peek().Item2.AppendLine(line);
						}
					}
				}
			}
			return dict;
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
	}
}
