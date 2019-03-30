using Albatross.CodeGen.TypeScript.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.TypeScript.Writer
{
    public class WriteClassProperty : IWriteObject<Property>
    {
        WriteTypeScriptType writeTypeScriptType;

        public WriteClassProperty(WriteTypeScriptType writeTypeScriptType) {
            this.writeTypeScriptType = writeTypeScriptType;
        }

        public string Write(Property t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(t.Name).Append(": ").Append(writeTypeScriptType.Write(t.Type)).Append(";");
            return sb.ToString();
        }
    }
}
