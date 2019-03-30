using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.TypeScript.Model
{
    public class TypeScriptType
    {
        public TypeScriptType(string name) {
            this.Name = name;
        }
        public TypeScriptType() { }

        public string Name { get; set; }
        public bool IsArray { get; set; }

        public readonly static TypeScriptType String = new TypeScriptType("string");
        public readonly static TypeScriptType Boolean  = new TypeScriptType("boolean");
        public readonly static TypeScriptType Number = new TypeScriptType("number");
        public readonly static TypeScriptType Any = new TypeScriptType("any");
        public readonly static TypeScriptType Void = new TypeScriptType("void");
    }
}
