# Introduction to Albatross CodeGen

The Albatross CodeGen Framework is a sophisticated code generation system designed to bridge the gap between different programming languages and platforms. It provides a unified approach to generating consistent, type-safe code across C#, TypeScript, and Python ecosystems.

## Core Concepts

### Code Elements
The framework is built around the concept of **Code Elements** - abstract representations of programming constructs that can be rendered into different target languages. These elements include:

- **Expressions**: Variables, literals, method calls, and other executable statements
- **Declarations**: Class definitions, method signatures, property declarations
- **Type Expressions**: Type references with support for generics and nullability
- **Code Nodes**: Hierarchical structures representing syntax trees

### Language Abstraction
Each target language (C#, Python, TypeScript) implements the core interfaces differently:

```csharp
public interface ICodeElement
{
    TextWriter Generate(TextWriter writer);
}
```

This abstraction allows the same logical code structure to be rendered appropriately for each language's syntax and conventions.

### Conversion Pipeline
The framework uses a conversion pipeline to transform .NET types and API definitions into target language constructs:

1. **Analysis**: Roslyn analyzes the source .NET assemblies
2. **Symbol Processing**: Type symbols are extracted and categorized
3. **Model Generation**: Internal models represent the API structure
4. **Code Generation**: Target language code is generated from models
5. **Output**: Generated files are written to the specified output directory

## Architecture Overview

The framework follows a modular architecture:

### Core Layer (`Albatross.CodeGen`)
- Base interfaces and abstractions
- Common code generation utilities
- Expression and declaration contracts

### Language Layers
- `Albatross.CodeGen.CSharp`: C# specific implementations
- `Albatross.CodeGen.Python`: Python specific implementations  
- `Albatross.CodeGen.TypeScript`: TypeScript specific implementations

### Web Client Layer (`Albatross.CodeGen.WebClient.*`)
- HTTP client proxy generation
- API controller analysis
- Language-specific web client implementations

### Command Line Tool (`Albatross.CodeGen.CommandLine`)
- CLI interface for code generation
- Project compilation and analysis
- Configuration management

### Symbol Providers (`Albatross.CodeGen.SymbolProviders`)
- Roslyn-based type analysis
- Metadata extraction utilities
- Symbol conversion helpers

## Type System

The framework provides comprehensive type system support:

- **Primitive Types**: Built-in type mappings (int, string, bool, etc.)
- **Generic Types**: Full generic type support with constraints
- **Nullable Types**: Proper nullability handling across languages
- **Collections**: Arrays, lists, dictionaries, and custom collections
- **Complex Types**: Classes, interfaces, enums, and inheritance hierarchies
- **Custom Attributes**: Attribute-based code generation customization

## Extension Points

The framework is designed for extensibility:

- **Type Converters**: Custom type conversion logic
- **Code Generators**: Custom code generation implementations
- **Source Lookups**: Custom module resolution strategies
- **Settings**: Configuration-based behavior modification

This modular design allows developers to extend the framework for specific use cases while maintaining consistency with the core architecture.