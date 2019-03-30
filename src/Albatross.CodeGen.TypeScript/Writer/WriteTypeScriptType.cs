using Albatross.CodeGen.TypeScript.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.TypeScript.Writer
{
    public class WriteTypeScriptType : IWriteObject<TypeScriptType>
    {
        public string Write(TypeScriptType t)
        {
            if (t.IsArray)
            {
                return $"{t.Name}[]";
            }
            else
            {
                return t.Name;
            }
        }
    }
}
