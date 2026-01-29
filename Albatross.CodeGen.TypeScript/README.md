# Albatross.CodeGen.TypeScript

TypeScript code generation library that provides expressions, declarations, and type converters for generating TypeScript source code from C# type symbols. Used by the web client generators to produce TypeScript interfaces, classes, and HTTP client services.

## Key Features

- TypeScript declarations: classes, interfaces, enums, methods, properties, constructors
- Expression support: arrow functions, await, binary expressions, string interpolation
- Import/export statement generation with ES6 module support
- Type converters for C# to TypeScript type mapping (numbers, dates, booleans, arrays, generics)
- Nullable type handling and union types
- Decorator expression support for Angular integration

## Quick Start

```csharp
using Albatross.CodeGen.TypeScript.Declarations;
using Albatross.CodeGen.TypeScript.Expressions;

// Create a TypeScript interface declaration
var interfaceDecl = new InterfaceDeclaration("User")
    .AddProperty(new PropertyDeclaration("id", new SimpleTypeExpression("number")))
    .AddProperty(new PropertyDeclaration("name", new SimpleTypeExpression("string")));

// Generate the TypeScript code
using var writer = new StringWriter();
interfaceDecl.Generate(writer);
// Output: export interface User { id: number; name: string; }
```

## Dependencies

- Albatross.CodeGen (project reference)
- Albatross.CodeGen.SymbolProviders (project reference)
- Albatross.CodeAnalysis 8.0.3
- Albatross.Collections 8.0.1
- Microsoft.CodeAnalysis.CSharp 5.0.0

## Source Code

[Albatross.CodeGen.TypeScript](https://github.com/RushuiGuan/codegen/tree/main/Albatross.CodeGen.TypeScript)

## NuGet Package

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.CodeGen.TypeScript)](https://www.nuget.org/packages/Albatross.CodeGen.TypeScript)
