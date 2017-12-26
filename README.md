# CodeGen

### Why
	- The project was created because I have created numerous code generators over the last few years.  
	Some of them are visual	studio text templates, others are console or powershell commands.  They were 
	just temperary solutions to get the job done at the time. So I created this project looking for a 
	permanent solution and hope others can join to create some interesting scenarios.
	
### Goal
	- The aim of this project is to simply use code generator to speed up softwware development.  It allows 
	users to setup scenarios that combine multiple generator with parameters and execute them.  There should 
	be no limitation on what it should generate other than text files.
		
### How to use it
	- In order to make it easier to use, I have created a UI that allows users to quickly setup scenarios 
	and execute them repeatly.  It has to be as easy as alt-tab to the UI, press F5 to generate and alt-tab 
	back to code and paste.
	
### Generator Engines
	Currently I am working on the following generators.  They are specific to the set of tools that I use.  The 
	Albatross.CodeGen.dll will be available in NuGet so that others can use it to create their own generators.
	
	- CRUD operations for SQL tables.
	- Dapper Command Definitions for Stored Procedures.
	- Generate C# POCO objects from sql tables
	- Creating C# WPF View Models from POCO objects
	- WebApi C# Proxy Generation
	
	
	
	
	
