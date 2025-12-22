# Framework Architecture

The Albatross CodeGen Framework follows a layered architecture that promotes modularity, extensibility, and language-agnostic code generation.

## Architectural Layers

### Core Abstraction Layer

At the foundation is the **Core Abstraction Layer** (`Albatross.CodeGen`), which defines the fundamental interfaces and contracts:

```csharp
public interface ICodeElement
{
    TextWriter Generate(TextWriter writer);
}

public interface ICodeNode : ICodeElement
{
    IEnumerable<ICodeNode> GetDescendants();
}

public interface IExpression : ICodeNode { }
public interface IDeclaration : ICodeNode { }
```

This layer provides:
- **Base Interfaces**: Common contracts for all code elements
- **Code Node Hierarchy**: Tree structure for representing syntax
- **Expression System**: Abstractions for executable code constructs
- **Type System**: Interfaces for type representation and conversion

### Language Implementation Layers

Each target language implements the core abstractions differently:

#### C# Implementation (`Albatross.CodeGen.CSharp`)
- C#-specific type expressions and declarations
- Namespace and using statement handling
- Generic type parameter support
- Nullable reference type handling
- C# keyword and operator definitions

#### Python Implementation (`Albatross.CodeGen.Python`)
- Python-specific syntax generators
- Import statement management
- Python naming convention handling
- Type hint generation
- Python-specific scope management

#### TypeScript Implementation (`Albatross.CodeGen.TypeScript`)
- TypeScript interface and type definitions
- ES6 module import/export handling
- Generic type constraints
- Optional property handling
- Union and intersection type support

### Domain-Specific Layers

#### Web Client Generation (`Albatross.CodeGen.WebClient.*`)
Specialized for HTTP client generation:
- **API Analysis**: Extracts controller and action information
- **Model Generation**: Creates DTOs from API models  
- **Client Generation**: Builds HTTP client proxies
- **Language Integration**: Adapts to each target language's HTTP patterns

#### Symbol Providers (`Albatross.CodeGen.SymbolProviders`)
Roslyn integration for .NET analysis:
- **Compilation Management**: Handles MSBuild project compilation
- **Symbol Extraction**: Extracts type and member information
- **Metadata Processing**: Processes attributes and documentation
- **Dependency Resolution**: Resolves type dependencies

### Application Layer

#### Command Line Tool (`Albatross.CodeGen.CommandLine`)
- **Command Processing**: Handles CLI commands and options
- **Configuration Management**: Loads and validates settings
- **Orchestration**: Coordinates the generation pipeline
- **Output Management**: Manages file generation and organization

## Component Interactions

### Generation Pipeline

1. **Project Analysis**
   ```
   MSBuild Project → Roslyn Compilation → Symbol Extraction
   ```

2. **Model Creation**
   ```
   Symbols → Domain Models → Language-Agnostic Representations
   ```

3. **Code Generation**
   ```
   Models → Language-Specific Generators → Output Files
   ```

### Dependency Injection

The framework uses dependency injection for modularity:

```csharp
services.AddCodeGen(assembly);  // Core services
services.AddCSharpCodeGen();    // C# specific
services.AddPythonCodeGen();    // Python specific
services.AddWebClientCodeGen(); // Web client services
```

## Design Principles

### Separation of Concerns
- **Analysis** vs **Generation**: Clear separation between code analysis and generation phases
- **Language Agnostic** vs **Language Specific**: Core logic remains language-neutral
- **Domain Models** vs **Output Models**: Separate representations for business logic and output formatting

### Extensibility
- **Interface-Driven**: All major components implement well-defined interfaces
- **Plugin Architecture**: New languages and generators can be added without modifying core
- **Configuration-Based**: Behavior can be customized through configuration

### Composability
- **Code Elements**: Small, composable pieces that can be combined
- **Generator Chains**: Multiple generators can process the same source
- **Modular Services**: Services can be mixed and matched as needed

### Type Safety
- **Strongly Typed**: Extensive use of generics and type constraints
- **Compile-Time Validation**: Many errors caught at compile time rather than runtime
- **Contract Enforcement**: Interfaces ensure consistent behavior across implementations

## Extension Points

The architecture provides several extension points:

### Custom Type Converters
```csharp
public interface ITypeConverter
{
    int Precedence { get; }
    bool TryConvert(ITypeSymbol symbol, 
                   IConvertObject<ITypeSymbol, ITypeExpression> factory, 
                   out ITypeExpression? expression);
}
```

### Custom Code Generators
```csharp
public interface IConvertObject<TFrom, TTo>
{
    TTo Convert(TFrom from);
}
```

### Custom Source Lookups
```csharp
public interface ISourceLookup
{
    bool TryGet(ITypeSymbol name, out ISourceExpression? module);
}
```

## Performance Considerations

### Compilation Caching
- Roslyn compilations are cached to avoid repeated parsing
- Symbol information is memoized during generation

### Incremental Generation  
- Framework supports analyzing only changed files
- Generated output can be compared to avoid unnecessary writes

### Memory Management
- Disposable patterns used for resource management
- Large syntax trees are processed in streaming fashion where possible

This architecture enables the framework to be both powerful and flexible, supporting complex code generation scenarios while remaining extensible for future requirements.