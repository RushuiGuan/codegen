using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.Core
{
    public interface ICustomCodeSection {
		string ApplyTo { get; }
		string Read(string name);
		void Write(string name, int tabCount, StringBuilder sb);
		void Load(string content);
	}
}
