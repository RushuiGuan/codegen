# CSharpWebClientSettings

This article documents the `CSharpWebClientSettings` type in `Albatross.CodeGen.WebClient.CSharp` and how to use it in a `codegen-settings.json` file.

## Where It Is Used

- Command: `codegen csharp2 web-client`
- Settings type: `Albatross.CodeGen.WebClient.CSharp.CSharpWebClientSettings`
- Generator classes that consume it:
  - `Albatross.CodeGen.WebClient.CSharp.ConvertWebApiToCSharpFile`
  - `Albatross.CodeGen.WebClient.CSharp.CreateHttpClientRegistrations`

## Minimal Example

```json
{
  "$schema": "codegen-settings.schema.json",
  "namespace": "MyCompany.MyApi.Client"
}
```

## Full Example

```json
{
  "$schema": "codegen-settings.schema.json",
  "namespace": "MyCompany.MyApi.Client",
  "useInterface": true,
  "useInternalProxy": true,
  "constructorSettings": {
    "*": {
      "customJsonSettings": "MyJsonSettings.Options"
    },
    "HealthController": {
      "omit": true
    }
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

### `namespace` (`string`, default: `"MyNamespace"`)

Target namespace for generated C# files.

### `useInterface` (`bool`, default: `false`)

When `true`, an interface is generated for each client and the generated client class implements it.

### `useInternalProxy` (`bool`, default: `false`)

When `true`, generated client classes use `internal` access instead of `public`.

This is commonly combined with `useInterface: true` so callers depend on interfaces while implementations stay internal.

### `constructorSettings` (`Dictionary<string, WebClientConstructorSettings>`)

Per-controller constructor customization.

- Key: controller symbol name (for example, `OrderController`)
- Special key: `*` acts as fallback for all controllers

`WebClientConstructorSettings` fields:

- `omit` (`bool`): when `true`, generated class constructor is omitted.
- `customJsonSettings` (`string?`): replacement expression for JSON options assignment in generated constructor.
- `compressionEncoding` (`string?`): defined in settings model, but not currently consumed by the C# web-client generator.

## Inherited Filter Settings

`CSharpWebClientSettings` inherits from `CodeGenSettings`:

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

### Force interface usage

```json
{
  "namespace": "My.Client",
  "useInterface": true,
  "useInternalProxy": true
}
```

### Omit constructor for selected controller

```json
{
  "constructorSettings": {
    "AuthController": {
      "omit": true
    }
  }
}
```

### Global JSON options plus per-controller override

```json
{
  "constructorSettings": {
    "*": {
      "customJsonSettings": "GlobalJson.Options"
    },
    "LegacyController": {
      "customJsonSettings": "LegacyJson.Options"
    }
  }
}
```
