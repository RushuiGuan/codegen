# Albatross.CodeGen

Core code generation engine providing abstract representations of code constructs that can be rendered into different target languages. This library defines the foundational interfaces and building blocks used by all language-specific generators in the Albatross CodeGen framework.

## Key Features

- Language-agnostic code element abstractions (`ICodeElement`, `ICodeNode`, `IExpression`, `IDeclaration`)
- Type conversion system with precedence-based converter ordering (`ITypeConverter`)
- Syntax tree node hierarchy for representing code structures
- Source lookup interfaces for module resolution
- Roslyn integration via `ICompilationFactory` for C# project analysis

## Quick Start

```csharp
using Albatross.CodeGen;

// Implement ICodeElement for custom code generation
public class MyExpression : ICodeElement {
    public TextWriter Generate(TextWriter writer) {
        writer.Write("myExpression");
        return writer;
    }
}

// Use the compilation factory to analyze a C# project
var factory = new CompilationFactory();
var compilation = await factory.CreateAsync("MyProject.csproj");
```

## Dependencies

- Microsoft.CodeAnalysis.CSharp 4.10.0
- Albatross.Reflection 8.0.7
- Albatross.Text 9.0.3
- Albatross.CodeAnalysis 8.0.3

## Source Code

[Albatross.CodeGen](https://github.com/RushuiGuan/codegen/tree/main/Albatross.CodeGen)

## NuGet Package

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.CodeGen)](https://www.nuget.org/packages/Albatross.CodeGen)
