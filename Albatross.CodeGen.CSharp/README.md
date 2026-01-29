# Albatross.CodeGen.CSharp

Legacy C# code generation library providing expressions, declarations, and code constructs for runtime C# code generation. This library predates the Roslyn-based approach but remains useful for simple runtime code generation scenarios.

## Key Features

- C# declarations: classes, interfaces, enums, methods, properties, fields, constructors
- Expression support: lambda, anonymous methods, switch, foreach, if/else, string interpolation
- Namespace and using statement generation
- Attribute expression support
- Access modifier keywords (public, private, protected, internal)
- Literal expressions for common types (int, string, bool, decimal, float, double)

## Quick Start

```csharp
using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;

// Create a C# class declaration
var classDecl = new ClassDeclaration("User")
    .AddProperty(new PropertyDeclaration("Id", new TypeExpression("int")))
    .AddProperty(new PropertyDeclaration("Name", new TypeExpression("string")));

// Generate the C# code
using var writer = new StringWriter();
classDecl.Generate(writer);
```

## Dependencies

- Albatross.CodeGen (project reference)
- Albatross.Collections 8.0.1

## Source Code

[Albatross.CodeGen.CSharp](https://github.com/RushuiGuan/codegen/tree/main/Albatross.CodeGen.CSharp)

## NuGet Package

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.CodeGen.CSharp)](https://www.nuget.org/packages/Albatross.CodeGen.CSharp)
