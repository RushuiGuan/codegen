# Albatross.CodeGen.CommandLine

Command-line tool for generating HTTP client proxies and DTOs from ASP.NET Core Web API projects. Analyzes C# projects using Roslyn and generates type-safe client code for TypeScript, Python, and C#.

## Key Features

- Generate TypeScript HTTP clients and DTOs for Angular applications
- Generate Python async HTTP clients and dataclass DTOs
- Generate C# HTTP client proxies
- Roslyn-based project analysis for accurate type information
- Configurable via JSON settings files
- Ad-hoc filtering to select specific controllers

## Installation

```bash
dotnet tool install --global Albatross.CodeGen.CommandLine
```

## Quick Start

```bash
# Generate TypeScript web client
codegen typescript web-client -p MyWebApi.csproj -o ./generated/typescript

# Generate TypeScript DTOs only
codegen typescript dto -p MyWebApi.csproj -o ./generated/typescript

# Generate Python web client
codegen python web-client -p MyWebApi.csproj -o ./generated/python

# Generate Python DTOs only
codegen python dto -p MyWebApi.csproj -o ./generated/python

# Generate C# web client proxy
codegen csharp web-client -p MyWebApi.csproj -o ./generated/csharp

# Use settings file
codegen typescript web-client -p MyWebApi.csproj -s codegen-settings.json -o ./generated
```

## Commands

| Command | Description |
|---------|-------------|
| `typescript web-client` | Generate TypeScript HTTP client services |
| `typescript dto` | Generate TypeScript interfaces/types |
| `typescript entry-point` | Generate TypeScript barrel exports |
| `python web-client` | Generate Python async HTTP clients |
| `python dto` | Generate Python dataclass models |
| `csharp web-client` | Generate C# HTTP client proxies |

## Dependencies

- Albatross.CodeGen.WebClient.CSharp (project reference)
- Albatross.CodeGen.WebClient.Python (project reference)
- Albatross.CodeGen.WebClient.TypeScript (project reference)
- Microsoft.CodeAnalysis.Workspaces.MSBuild 5.0.0
- NJsonSchema 11.5.2
- Albatross.CommandLine 8.0.1

## Prerequisites

- .NET 10.0 SDK or later
- Target project must build successfully before code generation

## Source Code

[Albatross.CodeGen.CommandLine](https://github.com/RushuiGuan/codegen/tree/main/Albatross.CodeGen.CommandLine)

## NuGet Package

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.CodeGen.CommandLine)](https://www.nuget.org/packages/Albatross.CodeGen.CommandLine)
