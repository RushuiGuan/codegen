using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
	/// <summary>
	/// Allow the creator to create a variable
	/// </summary>
    public interface ICreateVariable
    {
		void Create(object creator, Variable variable);
    }
}
