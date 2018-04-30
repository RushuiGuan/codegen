using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
	public class RenderDotNetType : IRenderDotNetType {
		Dictionary<Type, string> mapping = new Dictionary<Type, string>();

		void Init() {
			mapping.Add(typeof(string), "string");

			mapping.Add(typeof(short), "short");
			mapping.Add(typeof(int), "int");
			mapping.Add(typeof(long), "long");

			mapping.Add(typeof(ushort), "ushort");
			mapping.Add(typeof(uint), "uint");
			mapping.Add(typeof(ulong), "ulong");

			mapping.Add(typeof(float), "float");
			mapping.Add(typeof(decimal), "decimal");
			mapping.Add(typeof(char), "char");
			mapping.Add(typeof(double), "double");
			mapping.Add(typeof(bool), "bool");

			mapping.Add(typeof(byte), "byte");
			mapping.Add(typeof(sbyte), "sbyte");
		}

		public RenderDotNetType() {
			Init();
		}

		public void Render(StringBuilder sb, Type type) {
			string text;
			if (mapping.TryGetValue(type, out text)) {
				sb.Append(text);
			} else {
				sb.Append(type.FullName);
			}
		}
	}
}
