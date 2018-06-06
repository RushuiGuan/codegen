using System;
namespace Albatross.CodeGen.Generation
{
    public class DotNetProperty
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public string TypeOverride { get; set; }
    }
}
