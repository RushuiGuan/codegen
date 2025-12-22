---
_layout: landing
---

# Albatross CodeGen Framework

Welcome to the **Albatross CodeGen Framework** - a comprehensive multi-language code generation toolkit for .NET applications.

## What is Albatross CodeGen?

The Albatross CodeGen Framework is a powerful and extensible code generation system that enables you to:

- **Generate HTTP client proxies** for C#, TypeScript, and Python from ASP.NET Core Web APIs
- **Create strongly-typed DTOs** across multiple languages
- **Build custom code generators** using a flexible, composable architecture
- **Maintain consistency** across different language implementations of your APIs

## Key Features

- 🚀 **Multi-language Support**: Generate code for C#, TypeScript, and Python
- 🔧 **Extensible Architecture**: Build custom code generators using the core framework
- 📝 **Rich Type System**: Full support for generics, nullability, and complex types
- 🎯 **Web API Integration**: Seamless integration with ASP.NET Core controllers
- ⚙️ **Command Line Tool**: Easy-to-use CLI for automated code generation
- 📚 **Comprehensive Documentation**: Extensive XML documentation and examples

## Quick Start

1. **Install the tool**:
   ```bash
   dotnet tool install --global Albatross.CodeGen.CommandLine
   ```

2. **Generate TypeScript client**:
   ```bash
   codegen typescript web-client -p YourWebApi.csproj -o ./generated
   ```

3. **Generate Python DTOs**:
   ```bash
   codegen python dto -p YourWebApi.csproj -o ./generated
   ```

## Project Structure

The framework consists of several interconnected projects:

- **Core**: Base interfaces and abstractions
- **Language Generators**: C#, Python, TypeScript implementations  
- **Web Client**: HTTP client proxy generation
- **Command Line**: CLI tool for code generation
- **Symbol Providers**: Roslyn-based analysis components

## Getting Started

- [Introduction](articles/introduction.md) - Learn about the framework concepts
- [Getting Started Guide](articles/getting-started.md) - Step-by-step tutorial
- [API Reference](api/) - Complete API documentation

---

*Built with ❤️ by the Albatross team*