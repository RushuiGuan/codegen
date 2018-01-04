# CodeGen

### Why
I have created numerous code generators over the last few years.  Some of them are visual studio text templates, others are console or powershell commands.  They were just temperary solutions to get the job done at the time. Codes were short term and disgarded soon after.  So I created this project looking for a permanent solution and hope others can join to create some interesting scenarios.  
		
### Goal
The aim of this project is to simply use code generator to speed up software development.  It allows users to setup scenarios that combine multiple generator with parameters and execute them.  There should be no limitation on what it should generate other than text files.  
		
### How to use it
In order to make it easier to use, I have created powershell cmdlets that allows users to quickly setup scenarios and execute them repeatly.
	
### Generator Engines
Currently I am working on the following generators.  They are specific to the set of tools that I use.  The Albatross.CodeGen.dll will be available in NuGet so that others can use it to create their own generators.  

	- CRUD operations for SQL tables.
	- Dapper Command Definitions for Stored Procedures.
	- Generate C# POCO objects from sql tables
	- Creating C# WPF View Models from POCO objects
	- WebApi C# Proxy Generation
	
### Concept and Design
**Albatross.CodeGen.ICodeGenerator** interface is reponsible for the code generation logic.  Implement this interface to create new generators.  **All code generator MUST be THREADSAFE Singleton objects!!!**
- **Name**; Each generator has a unique name that will be used as identitication.  
- **Target**; Target indicates what kind of text is being generated.  Some examples could be C#, sql, javascript etc.  
- **SourceType**; SourceType indicates the Class Type of the input source when the generator is invoked.  Currently we have:
	* Albatross.CodeGen.Database.Table; A sql database table
	* Albatross.CodeGen.Database.StoredProcedure; A sql stored procedure
	* Albatross.CodeGen.CSharp.ObjectType; A C# class type
- **Category** and **Description**; They are both informational properties.
- **Build(text, source, options, factory)**; Call the  method to invoke the generator.  
	* text is a string builder instance.
	* source is the source of data that the generator is running against.
	* Option is the secondary parameter allows the adjustment of code generation behavior.
	* Factory is a instance of the ICodeGeneratorFactory.  It will allow the generator to retrieve other generators.

**CodeGeneratorAttribute**; Any ICodeGenerator implementation with this attribute will be registered automatically when IConfigurableCodeGenFactory.Register(Assembly) is called.

**Composite**; A composite generator contains multiple generators of the same target.  When invoked, its components will be invoked sequentially.  The same source and option will be passed into each of its components.  A composite can be created by creating instances of the concrete CompositeCodeGenerator class.  The created instances will need to be registered manually by calling IConfigurableCodeGenFactory.Register(IEnumerable<ICodeGenerator>).  Sometimes it might be better to create composites by deriving directly from the CompositeCodeGenerator class and mark it using the CodeGeneratorAttribute.
