# CodeGen

### Why
I have created numerous code generators over the last few years.  Some of them are visual studio text templates, others are console or powershell commands.  They were just temperary solutions to get the job done at the time. Codes were short term and disgarded soon after.  So I created this project looking for a permanent solution and hope others can join to create some interesting code generators.  

### Goal
The aim of this project is to simply use code generator to speed up software development.  It allows users to setup composite generators that combine multiple generators using a hierarical tree.
		
### How to use it
In order to make it easier to use, I have created powershell cmdlets that allows users to quickly setup composites and execute them repeatly.
	
### Concept and Design
A generator is a set of code that will be run against a source and options.  Generators should implement Albatross.CodeGen.ICodeGenerator<T, O> interface. T is the source type and O is the option type.  



A generator class can be marked by the Albatross.CodeGen.CodeGeneratorAttribute so that it can be registered automatically.  

The source is the entity that the generator will run against; for example, a database table or a .Net type.  The option is the choices that could affect the outcome of code generation against a source.
**Albatross.CodeGen.ICodeGenerator** interface is reponsible for the code generation logic.  Implement this interface to create new generators.  
- **Name**; Each generator has a unique name that will be used as identitication.  
- **SourceType**; SourceType indicates the Class Type of the input source when the generator is invoked.
- **Target**; Target indicates what kind of text is being generated.  Some examples could be C#, sql, javascript etc. 
- **Category** and **Description**; They are both informational properties.
- **Build(text, source, options) as objects**; Call the  method to invoke the generator.  
	* text is a string builder instance.
	* source is the source of data that the generator is running against.
	* Option is the secondary parameter allows the adjustment of code generation behavior.
	* the method will return the list of generators used by this call.

### Generator Engines
Currently I am working on the following generators.  They are specific to the set of tools that I use.  The Albatross.CodeGen.dll will be available in NuGet so that others can use it to create their own generators.  

	- CRUD operations for SQL tables.
	- Dapper Command Definitions for Stored Procedures.
	- Generate C# POCO objects from sql tables
	- Creating C# WPF View Models from POCO objects
	- WebApi C# Proxy Generation
	


**CodeGeneratorAttribute**; Any ICodeGenerator implementation with this attribute will be registered automatically when IConfigurableCodeGenFactory.Register(Assembly) is called.

**Composite**; A composite generator contains multiple generators of the same target.  When invoked, its components will be invoked sequentially.  The same source and option will be passed into each of its components.  A composite can be created by creating instances of the concrete CompositeCodeGenerator class.  The created instances will need to be registered manually by calling IConfigurableCodeGenFactory.Register(IEnumerable<ICodeGenerator>).  Sometimes it might be better to create composites by deriving directly from the CompositeCodeGenerator class and mark it using the CodeGeneratorAttribute.
