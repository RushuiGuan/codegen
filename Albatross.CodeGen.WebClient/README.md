# Albatross.CodeGen.WebClient

Core library for generating HTTP client proxies from ASP.NET Core controller classes. Provides models, symbol analysis, and shared logic used by language-specific web client generators (C#, TypeScript, Python).

## Key Features

- ASP.NET Core controller analysis using Roslyn symbol walkers
- Model extraction for controllers, methods, parameters, and DTOs
- Route segment parsing and parameter binding detection
- Enum and class inheritance support with JsonDerivedType handling
- Configurable symbol filtering with include/exclude patterns
- Settings-based customization for code generation behavior

## Quick Start

```csharp
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;

// Analyze controllers from a compilation
var walker = new ApiControllerClassWalker(compilation, settings);
var controllers = walker.GetControllerInfos();

// Extract DTOs from the same compilation
var dtoWalker = new DtoClassEnumWalker(compilation, settings);
var dtos = dtoWalker.GetDtoClasses();
var enums = dtoWalker.GetEnums();
```

## Dependencies

- Albatross.CodeGen.CSharp (project reference)
- Albatross.CodeGen.Python (project reference)
- Albatross.CodeGen.TypeScript (project reference)
- Microsoft.CodeAnalysis.CSharp 5.0.0
- Albatross.CodeAnalysis 8.0.3
- Microsoft.Extensions.DependencyInjection.Abstractions 10.0.1

## Source Code

[Albatross.CodeGen.WebClient](https://github.com/RushuiGuan/codegen/tree/main/Albatross.CodeGen.WebClient)

## NuGet Package

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.CodeGen.WebClient)](https://www.nuget.org/packages/Albatross.CodeGen.WebClient)
