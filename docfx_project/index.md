# Albatross CodeGen

A multi-language code generation framework for .NET that generates HTTP client proxies and DTOs from ASP.NET Core Web APIs. Supports C#, TypeScript, and Python output with full type safety and proper language idioms.

## Key Features

- Generate HTTP client proxies for C#, TypeScript, and Python from ASP.NET Core controllers
- Create strongly-typed DTOs across multiple languages with proper type mapping
- Roslyn-based code analysis for accurate type information and generics support
- Extensible architecture with customizable type converters and source lookups
- Command-line tool for automated code generation in build pipelines

## Quick Start

```bash
# Install the CLI tool
dotnet tool install --global Albatross.CodeGen.CommandLine

# Generate TypeScript client from your Web API
codegen typescript web-client -p YourWebApi.csproj -o ./generated

# Generate Python client
codegen python web-client -p YourWebApi.csproj -o ./generated
```

## NuGet Packages

| Package | Description |
|---------|-------------|
| [Albatross.CodeGen](https://www.nuget.org/packages/Albatross.CodeGen) | Core code generation engine with abstract code representations |
| [Albatross.CodeGen.TypeScript](https://www.nuget.org/packages/Albatross.CodeGen.TypeScript) | TypeScript declarations, expressions, and type converters |
| [Albatross.CodeGen.Python](https://www.nuget.org/packages/Albatross.CodeGen.Python) | Python declarations, expressions, and type converters |
| [Albatross.CodeGen.CSharp](https://www.nuget.org/packages/Albatross.CodeGen.CSharp) | C# declarations and expressions for runtime code generation |
| [Albatross.CodeGen.WebClient](https://www.nuget.org/packages/Albatross.CodeGen.WebClient) | ASP.NET Core controller analysis and model extraction |
| [Albatross.CodeGen.CommandLine](https://www.nuget.org/packages/Albatross.CodeGen.CommandLine) | CLI tool for code generation |

## Documentation

- [Introduction](articles/introduction.md) - Framework concepts and architecture
- [Getting Started](articles/getting-started.md) - Step-by-step tutorial
- [Configuration](articles/configuration.md) - Settings and customization options
- [Examples](articles/examples.md) - Real-world usage scenarios
- [API Reference](api/) - Complete API documentation

## Source Code

[GitHub Repository](https://github.com/RushuiGuan/codegen)
