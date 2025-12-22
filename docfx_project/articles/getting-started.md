# Getting Started with Albatross CodeGen

This guide will walk you through setting up and using the Albatross CodeGen Framework to generate HTTP client code from your ASP.NET Core Web APIs.

## Prerequisites

- .NET 8.0 or later
- An ASP.NET Core Web API project
- Basic understanding of HTTP APIs and client generation concepts

## Installation

### Install as Global Tool

```bash
dotnet tool install --global Albatross.CodeGen.CommandLine
```

### Verify Installation

```bash
codegen --help
```

## Basic Usage

### 1. Analyze Your Web API Project

First, ensure your Web API project compiles successfully:

```bash
cd YourWebApiProject
dotnet build
```

### 2. Generate TypeScript Client

Generate a TypeScript HTTP client for your API:

```bash
codegen typescript web-client -p YourWebApi.csproj -o ./generated/typescript
```

This will create:
- Type definitions for your DTOs
- HTTP client classes for your controllers
- Proper import/export statements

### 3. Generate Python Client

Generate a Python HTTP client:

```bash
codegen python web-client -p YourWebApi.csproj -o ./generated/python
```

This will create:
- Python classes for DTOs with proper type hints
- Async HTTP client implementations
- Proper module structure

### 4. Generate C# Client

Generate a C# HTTP client proxy:

```bash
codegen csharp2 web-client -p YourWebApi.csproj -o ./generated/csharp
```

## Advanced Configuration

### Using Settings Files

Create a `codegen-settings.json` file to customize generation:

```json
{
  "namespace": "MyCompany.ApiClients",
  "outputPath": "./Generated",
  "includePatterns": ["*Controller"],
  "excludePatterns": ["*TestController"],
  "generateAsync": true,
  "includeSummaryComments": true
}
```

Use the settings file:

```bash
codegen typescript web-client -p YourWebApi.csproj -s codegen-settings.json
```

### Filtering Controllers

Use ad-hoc filters to limit which controllers are processed:

```bash
codegen typescript web-client -p YourWebApi.csproj -c "User*Controller" -o ./generated
```

## Project Structure Example

After running code generation, your project structure might look like:

```
YourWebApiProject/
├── Controllers/
│   ├── UsersController.cs
│   └── ProductsController.cs
├── Models/
│   ├── User.cs
│   └── Product.cs
├── generated/
│   ├── typescript/
│   │   ├── models/
│   │   │   ├── user.ts
│   │   │   └── product.ts
│   │   ├── clients/
│   │   │   ├── users-client.ts
│   │   │   └── products-client.ts
│   │   └── index.ts
│   ├── python/
│   │   ├── models/
│   │   │   ├── user.py
│   │   │   └── product.py
│   │   ├── clients/
│   │   │   ├── users_client.py
│   │   │   └── products_client.py
│   │   └── __init__.py
│   └── csharp/
│       ├── Models/
│       ├── Clients/
│       └── ApiClient.cs
```

## Using Generated Code

### TypeScript Example

```typescript
import { UsersClient, User } from './generated/typescript';

const client = new UsersClient('https://api.example.com');
const users: User[] = await client.getUsers();
```

### Python Example

```python
from generated.python import UsersClient, User
from typing import List

client = UsersClient('https://api.example.com')
users: List[User] = await client.get_users()
```

### C# Example

```csharp
using Generated.CSharp;

var client = new UsersClient(httpClient);
var users = await client.GetUsersAsync();
```

## Next Steps

1. **Explore the API Reference**: Check out the [API documentation](../api/) for detailed information about all available types and methods.

2. **Customize Generation**: Learn about advanced configuration options and custom type converters.

3. **Integration**: Integrate code generation into your build pipeline for automatic updates.

4. **Extensions**: Explore creating custom code generators for specific scenarios.

## Troubleshooting

### Common Issues

1. **Compilation Errors**: Ensure your Web API project builds successfully before running code generation.

2. **Missing Types**: Check that all referenced packages are properly installed and restored.

3. **Output Directory**: Ensure the output directory exists or can be created by the tool.

4. **Permissions**: Verify you have write permissions to the output directory.

For more detailed troubleshooting, check the tool's verbose output:

```bash
codegen typescript web-client -p YourWebApi.csproj -o ./generated --verbosity detailed
```