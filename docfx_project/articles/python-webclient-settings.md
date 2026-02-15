# PythonWebClientSettings

This article documents the `PythonWebClientSettings` type in `Albatross.CodeGen.WebClient.Python` and how to configure Python web-client generation.

## Where It Is Used

- Command: `codegen python web-client`
- Settings type: `Albatross.CodeGen.WebClient.Python.PythonWebClientSettings`
- Generator components that consume it:
  - `Albatross.CodeGen.WebClient.Python.ConvertControllerModelToPythonFile`
  - `Albatross.CodeGen.WebClient.Python.ConvertDtoClassModelToDataClass`
  - `Albatross.CodeGen.WebClient.Python.ConvertDtoClassPropertyModelToFieldDeclaration`
  - `Albatross.CodeGen.WebClient.Python.MappedTypeConverter`
  - `Albatross.CodeGen.Python.DefaultPythonSourceLookup` (via `namespaceModuleMapping`)

## Minimal Example

```json
{
  "$schema": "codegen-settings.schema.json",
  "namespaceModuleMapping": {
    "MyApi.Contracts": "models"
  }
}
```

## Full Example

```json
{
  "$schema": "codegen-settings.schema.json",
  "async": true,
  "namespaceModuleMapping": {
    "MyApi.Contracts": "models",
    "MyApi.Shared": "shared_models"
  },
  "typeMapping": {
    "System.Uri": "AnyUrl,pydantic"
  },
  "propertyNameMapping": {
    "MyApi.Contracts.UserDto.DisplayName": "display_name"
  },
  "httpClientClassNameMapping": {
    "MyApi.Web.Controllers.UsersController": "UsersApi"
  },
  "dtoClassNameMapping": {
    "MyApi.Contracts.UserDto": "UserModel"
  },
  "controllerFilters": [
    {
      "exclude": "InternalController$"
    }
  ],
  "controllerMethodFilters": [
    {
      "exclude": "DebugOnly$"
    }
  ],
  "dtoEnumFilters": [
    {
      "exclude": "^MyApi\\.Contracts\\.Internal\\."
    }
  ]
}
```

## Property Reference

### `async` (`bool`, default: `false`)

Controls HTTP client runtime style:

- `true`: async client (`httpx.AsyncClient`), generated methods use `async/await`
- `false`: sync client (`requests.Session`)

### `namespaceModuleMapping` (`Dictionary<string, string>`)

Maps C# namespace prefixes to Python module paths used for imports.

- Key: namespace prefix
- Value: python module path

Matching is prefix-based on full symbol name.

### `typeMapping` (`Dictionary<string, string>`)

Overrides type conversion for specific C# type full names.

- Key: C# type full name
- Value: parsed Python type identifier

Value format supports either:

- `Name` (local identifier)
- `Name,module.path` (qualified import type)

Example:

```json
{
  "typeMapping": {
    "System.Uri": "AnyUrl,pydantic"
  }
}
```

### `propertyNameMapping` (`Dictionary<string, string>`)

Overrides generated Python field names for DTO properties.

- Key: property full name
- Value: desired field name (then converted to `snake_case`)

### `httpClientClassNameMapping` (`Dictionary<string, string>`)

Overrides generated Python client class names.

- Key: controller full name
- Value: client class name

If no mapping exists, the default is `<ControllerName>Client`.

### `dtoClassNameMapping` (`Dictionary<string, string>`)

Overrides generated Python DTO class names.

- Key: DTO class full name
- Value: class name

### `constructorSettings` (`Dictionary<string, WebClientConstructorSettings>`)

Available in the settings model, but currently not consumed by the Python web-client generator.

`WebClientConstructorSettings` fields:

- `omit` (`bool`)
- `customJsonSettings` (`string?`)
- `compressionEncoding` (`string?`)

## Inherited Filter Settings

`PythonWebClientSettings` inherits from `CodeGenSettings`:

- `controllerFilters`
- `controllerMethodFilters`
- `dtoEnumFilters`

Each item is a `SymbolFilterPatterns` object:

- `exclude`: regex pattern
- `include`: regex pattern

Filter behavior:

- Exclude is evaluated first.
- Include can override an exclude match.
- If neither matches, symbol is kept.

## Common Patterns

### Async clients everywhere

```json
{
  "async": true
}
```

### Map C# namespaces to Python modules

```json
{
  "namespaceModuleMapping": {
    "MyApi.Contracts": "models",
    "MyApi.Contracts.Admin": "models.admin"
  }
}
```

### Rename selected generated DTO and client classes

```json
{
  "dtoClassNameMapping": {
    "MyApi.Contracts.OrderDto": "OrderModel"
  },
  "httpClientClassNameMapping": {
    "MyApi.Web.Controllers.OrdersController": "OrdersApi"
  }
}
```
