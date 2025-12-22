# Examples and Use Cases

This section provides practical examples of using the Albatross CodeGen Framework in real-world scenarios.

## Basic Examples

### Simple Controller Generation

Given this ASP.NET Core controller:

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        // Implementation
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        // Implementation
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserRequest request)
    {
        // Implementation
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

### Generated TypeScript Client

**Command:**
```bash
codegen typescript web-client -p MyWebApi.csproj -o ./generated/typescript
```

**Generated files:**

**models/user.ts:**
```typescript
export interface User {
  id: number;
  name: string;
  email: string;
  createdAt: Date;
}

export interface CreateUserRequest {
  name: string;
  email: string;
}
```

**clients/users-client.ts:**
```typescript
import { User, CreateUserRequest } from '../models/user';

export class UsersClient {
  constructor(private baseUrl: string = '') {}

  async getUsers(): Promise<User[]> {
    const response = await fetch(`${this.baseUrl}/api/users`);
    return await response.json();
  }

  async getUser(id: number): Promise<User> {
    const response = await fetch(`${this.baseUrl}/api/users/${id}`);
    return await response.json();
  }

  async createUser(request: CreateUserRequest): Promise<User> {
    const response = await fetch(`${this.baseUrl}/api/users`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request)
    });
    return await response.json();
  }
}
```

### Generated Python Client

**Command:**
```bash
codegen python web-client -p MyWebApi.csproj -o ./generated/python
```

**models/user.py:**
```python
from dataclasses import dataclass
from datetime import datetime
from typing import Optional

@dataclass
class User:
    id: int
    name: str
    email: str
    created_at: datetime

@dataclass
class CreateUserRequest:
    name: str
    email: str
```

**clients/users_client.py:**
```python
import aiohttp
from typing import List
from ..models.user import User, CreateUserRequest

class UsersClient:
    def __init__(self, base_url: str = ""):
        self.base_url = base_url
    
    async def get_users(self) -> List[User]:
        async with aiohttp.ClientSession() as session:
            async with session.get(f"{self.base_url}/api/users") as response:
                data = await response.json()
                return [User(**item) for item in data]
    
    async def get_user(self, id: int) -> User:
        async with aiohttp.ClientSession() as session:
            async with session.get(f"{self.base_url}/api/users/{id}") as response:
                data = await response.json()
                return User(**data)
    
    async def create_user(self, request: CreateUserRequest) -> User:
        async with aiohttp.ClientSession() as session:
            async with session.post(
                f"{self.base_url}/api/users",
                json=request.__dict__
            ) as response:
                data = await response.json()
                return User(**data)
```

## Advanced Examples

### Complex Types with Inheritance

**Source C# Models:**
```csharp
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
}

public enum ProductCategory
{
    Electronics,
    Clothing,
    Books,
    Home
}

public class ProductWithReviews : Product
{
    public List<Review> Reviews { get; set; } = new();
    public double AverageRating { get; set; }
}

public class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
```

**Generated TypeScript with Inheritance:**
```typescript
export interface BaseEntity {
  id: number;
  createdAt: Date;
  updatedAt: Date;
}

export enum ProductCategory {
  Electronics = 0,
  Clothing = 1,
  Books = 2,
  Home = 3
}

export interface Product extends BaseEntity {
  name: string;
  price: number;
  category: ProductCategory;
}

export interface Review {
  id: number;
  productId: number;
  reviewerName: string;
  rating: number;
  comment: string;
}

export interface ProductWithReviews extends Product {
  reviews: Review[];
  averageRating: number;
}
```

### Generic Types

**Source C# Controller:**
```csharp
[ApiController]
public class GenericController<T> : ControllerBase where T : BaseEntity
{
    [HttpGet]
    public async Task<PagedResult<T>> GetPaged(
        int page = 1, 
        int pageSize = 10,
        string? search = null)
    {
        // Implementation
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool HasNext => Page * PageSize < TotalCount;
    public bool HasPrevious => Page > 1;
}
```

**Generated TypeScript:**
```typescript
export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  hasNext: boolean;
  hasPrevious: boolean;
}

export class GenericClient<T extends BaseEntity> {
  constructor(private baseUrl: string, private endpoint: string) {}

  async getPaged(
    page: number = 1,
    pageSize: number = 10,
    search?: string
  ): Promise<PagedResult<T>> {
    const params = new URLSearchParams();
    params.append('page', page.toString());
    params.append('pageSize', pageSize.toString());
    if (search) params.append('search', search);
    
    const response = await fetch(`${this.baseUrl}/${this.endpoint}?${params}`);
    return await response.json();
  }
}
```

### Custom Configuration Example

**Settings File (codegen.json):**
```json
{
  "namespace": "MyCompany.ApiClients",
  "includePatterns": ["*Controller"],
  "excludePatterns": ["*TestController", "*AdminController"],
  "typeMapping": {
    "System.Guid": "string",
    "System.DateTimeOffset": "Date"
  },
  "templates": {
    "fileHeader": "/* eslint-disable */\n// Auto-generated by Albatross CodeGen\n// Date: {{date}}\n",
    "classHeader": "/**\n * {{summary}}\n * Generated from: {{sourceType}}\n */"
  },
  "validation": {
    "requireDocumentation": true,
    "warnOnMissingTypes": false
  }
}
```

**Usage:**
```bash
codegen typescript web-client -p MyWebApi.csproj -s codegen.json -o ./src/generated
```

## Integration Examples

### Build Pipeline Integration

**MSBuild Integration (.csproj):**
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  
  <Target Name="GenerateClients" BeforeTargets="Build">
    <Exec Command="codegen typescript web-client -p $(MSBuildProjectFile) -o ./ClientApp/src/generated" />
  </Target>
</Project>
```

**GitHub Actions Workflow:**
```yaml
name: Generate API Clients

on:
  push:
    branches: [ main, develop ]

jobs:
  generate:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    
    - name: Install CodeGen Tool
      run: dotnet tool install --global Albatross.CodeGen.CommandLine
    
    - name: Generate TypeScript Client
      run: codegen typescript web-client -p MyWebApi.csproj -o ./frontend/src/generated
    
    - name: Generate Python Client
      run: codegen python web-client -p MyWebApi.csproj -o ./python-client/src/generated
    
    - name: Commit generated files
      run: |
        git config --local user.email "action@github.com"
        git config --local user.name "GitHub Action"
        git add ./frontend/src/generated ./python-client/src/generated
        git diff --staged --quiet || git commit -m "Update generated API clients"
        git push
```

### Docker Integration

**Dockerfile:**
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install CodeGen tool
RUN dotnet tool install --global Albatross.CodeGen.CommandLine
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy source and generate clients
COPY . /src
WORKDIR /src
RUN dotnet build MyWebApi.csproj
RUN codegen typescript web-client -p MyWebApi.csproj -o ./generated/typescript
RUN codegen python web-client -p MyWebApi.csproj -o ./generated/python

# Continue with application build...
```

## Best Practices Examples

### Organizing Generated Code

**Recommended Structure:**
```
src/
├── generated/           # All generated code
│   ├── typescript/
│   │   ├── models/     # Data models
│   │   ├── clients/    # API clients
│   │   └── index.ts    # Barrel exports
│   ├── python/
│   │   ├── models/
│   │   ├── clients/
│   │   └── __init__.py
│   └── csharp/
│       ├── Models/
│       └── Clients/
├── custom/             # Custom code
└── app/               # Application code
```

### Error Handling Patterns

**Generated TypeScript with Error Handling:**
```typescript
export class ApiError extends Error {
  constructor(public status: number, message: string, public response?: any) {
    super(message);
  }
}

export class UsersClient {
  async getUser(id: number): Promise<User> {
    const response = await fetch(`${this.baseUrl}/api/users/${id}`);
    
    if (!response.ok) {
      const errorData = await response.text();
      throw new ApiError(response.status, response.statusText, errorData);
    }
    
    return await response.json();
  }
}
```

These examples demonstrate the flexibility and power of the Albatross CodeGen Framework across various scenarios and integration patterns.