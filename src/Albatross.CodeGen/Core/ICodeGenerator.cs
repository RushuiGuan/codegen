using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core {
	/// <summary>
	/// This interface is responsible for all code genration implementation.  A code generator requires a) A source; b) an option.  The source indicates what the generator is running against; for example, a database table or a .net type.  It defines the input of the generator.
	/// The option indicates the supported options for the particular generator.  It could be the name of the generated class or whether the javascript output should use camel case.  Therefore it influences the output of the generator.  CodeGenerator implementation are not expected to be 
	/// thread safe, since new instances will be created for each execution.  Instances of this interface will be created using an Ioc container.  Therefore its dependencies should be declared in its constructor.  Only a single constructor is supported.
	/// </summary>
	/// <typeparam name="T">Source type</typeparam>
	/// <typeparam name="O">Option type</typeparam>
	public interface ICodeGenerator<in T, in O> : ICodeGenerator where T:class where O:class {

		/// <summary>
		/// The main code generation method.
		/// </summary>
		/// <param name="sb"></param>
		/// <param name="source"></param>
		/// <param name="option"></param>
		/// <returns></returns>
		IEnumerable<object> Generate(StringBuilder sb, T source, O option);
	}


	public interface ICodeGenerator {
		/// <summary>
		/// Normal a generator is run against a source and doesn't need to contain states.  But there are exceptions such as <see cref="Albatross.CodeGen.MultiSourceCompositeCodeGenerator{T, O}"/> or <see cref="Albatross.CodeGen.StaticCodeGenerator"/>.  These
		/// generators need to be configured because their main function is not to generate code using the source but to generate code based on a data configuration.
		/// </summary>
		/// <param name="data"></param>
		void Configure(object data);
		event Func<StringBuilder, IEnumerable<object>> Yield;

		IEnumerable<object> Generate(StringBuilder sb, IDictionary<string, string> customCode, object source, object option);
	}
}
