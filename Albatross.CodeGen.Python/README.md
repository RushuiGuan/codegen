# Albatross.CodeGen.Python

Python code generation library that provides expressions, declarations, and type converters for generating Python source code from C# type symbols. Used by the web client generators to produce Python dataclasses, type hints, and async HTTP client implementations.

## Key Features

- Python declarations: classes, methods, enums, constructors, fields, properties
- Expression support: await, decorators, list comprehensions, dictionaries, tuples
- Import statement generation with proper module organization
- Type converters for C# to Python type mapping (int, str, bool, datetime, Decimal, etc.)
- Full type hint generation for modern Python
- Async/await support for async HTTP clients

## Quick Start

```csharp
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;

// Create a Python class declaration
var classDecl = new ClassDeclaration("User")
    .AddField(new FieldDeclaration("id", new SimpleTypeExpression("int")))
    .AddField(new FieldDeclaration("name", new SimpleTypeExpression("str")));

// Generate the Python code
using var writer = new StringWriter();
classDecl.Generate(writer);
```

## Dependencies

- Albatross.CodeGen (project reference)
- Albatross.CodeGen.SymbolProviders (project reference)
- Albatross.CodeAnalysis 8.0.3
- Albatross.Collections 8.0.1
- Microsoft.CodeAnalysis.CSharp 5.0.0

## Source Code

[Albatross.CodeGen.Python](https://github.com/RushuiGuan/codegen/tree/main/Albatross.CodeGen.Python)

## NuGet Package

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.CodeGen.Python)](https://www.nuget.org/packages/Albatross.CodeGen.Python)
