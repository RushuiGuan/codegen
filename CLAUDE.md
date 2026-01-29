# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Albatross CodeGen is a multi-language code generation framework for .NET that generates HTTP client proxies and DTOs from ASP.NET Core controllers. It supports C#, TypeScript (Angular), and Python (async) output.

## Build and Test Commands

```bash
# Build the solution
dotnet build codegen.sln

# Run unit tests
dotnet test Albatross.CodeGen.UnitTest/Albatross.CodeGen.UnitTest.csproj

# Run a single test
dotnet test Albatross.CodeGen.UnitTest/Albatross.CodeGen.UnitTest.csproj --filter "FullyQualifiedName~TestMethodName"

# Build documentation (requires docfx)
docfx docfx_project/docfx.json --serve

# Install CLI tool locally from artifacts
dotnet tool install --global --add-source artifacts Albatross.CodeGen.CommandLine
```

## CLI Commands

The `codegen` CLI tool supports:
- `codegen csharp web-client` - Generate C# HTTP client proxies
- `codegen typescript web-client` - Generate TypeScript Angular services
- `codegen typescript dto` - Generate TypeScript DTOs
- `codegen python web-client` - Generate async Python HTTP clients
- `codegen python dto` - Generate Python DTOs

## Architecture

### Code Generation Pipeline
1. **Roslyn Compilation** - C# source is analyzed using Microsoft.CodeAnalysis
2. **Symbol Extraction** - Controllers and DTOs are extracted via `ICompilationFactory`
3. **Model Conversion** - Symbols converted to language-agnostic code nodes (`ICodeNode`, `IExpression`, `IDeclaration`)
4. **Type Mapping** - `ITypeConverter` maps C# types to target language equivalents
5. **Code Emission** - Language-specific generators produce output files

### Key Abstractions
- `ICodeNode` - Base representation of code constructs
- `IExpression` - Code expressions (method calls, member access)
- `IDeclaration` - Code declarations (classes, properties, methods)
- `ITypeConverter` - Cross-language type mapping
- `IConvertObject` - Converts Roslyn symbols to code nodes

### Project Structure
- `Albatross.CodeGen/` - Core engine with abstract code representations
- `Albatross.CodeGen.CommandLine/` - CLI entry point and command handlers
- `Albatross.CodeGen.SymbolProviders/` - Roslyn symbol analysis
- `Albatross.CodeGen.WebClient/` - Base web client generation logic
- `Albatross.CodeGen.WebClient.{CSharp,Python,TypeScript}/` - Language-specific HTTP client generators
- `Albatross.CodeGen.{Python,TypeScript}/` - Language-specific keywords, declarations, expressions
- `Test.WebApi/` - Sample ASP.NET Core API for testing
- `Test.Proxy/` - Generated C# client example

### Entry Point
`Albatross.CodeGen.CommandLine/Program.cs` - Configures DI based on command type and dispatches to appropriate handler.

## Configuration

- `Directory.Build.props` - Shared build properties (version 9.0.2, nullable enabled, LangVersion latest)
- Targets: netstandard2.0 and net10.0
- Test framework: XUnit with FluentAssertions

## Dependencies

Key external dependencies:
- Microsoft.CodeAnalysis.CSharp - Roslyn compiler services
- NJsonSchema - JSON schema for settings validation
- System.CommandLine - CLI framework
- Albatross.* packages - Shared utilities (Reflection, Text, CodeAnalysis, Logging)
