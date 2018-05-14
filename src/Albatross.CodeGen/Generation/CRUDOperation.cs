using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Generation
{
    public class CRUDOperation
    {
		public Table Table { get; set; }
		public Procedure Procedure { get; set; }
    }
}
