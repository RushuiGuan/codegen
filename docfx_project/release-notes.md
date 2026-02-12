# Release Notes

## Version 9.0.2

Current release with full support for .NET 10.0.

### Breaking Changes - C# Web Client Generation

The C# web client code generation has been completely redesigned with the following changes:

#### Class Naming
- Generated client classes are now named `*Client` instead of `*ProxyService` (e.g., `HttpMethodTestClient` instead of `HttpMethodTestProxyService`)

#### Base Class and Dependencies
- Generated clients **no longer inherit from `ClientBase`**
- Dependency changed from `Albatross.WebClient` to `Albatross.Http`
- Constructor no longer requires an `ILogger` parameter - only `HttpClient` is needed
- Each client now manages its own `HttpClient` and `JsonSerializerOptions` fields

#### Method Signatures
- All async methods now include a `CancellationToken` parameter as the last argument

#### Request Building
- Replaced the old `CreateRequest`/`CreateJsonRequest` pattern with a new fluent `RequestBuilder` API:
  ```csharp
  var builder = new RequestBuilder()
      .WithMethod(HttpMethod.Get)
      .WithRelativeUrl($"{ControllerPath}/{id}");
  ```
- Query string handling uses `builder.AddQueryString()` and `builder.AddQueryStringIfSet()` instead of `NameValueCollection`

#### Response Handling
- Uses new HttpClient extension methods: `client.Send<T>()`, `client.Execute<T>()`, `client.ExecuteOrThrow<T>()`, `client.ExecuteOrThrowStruct<T>()`
- Methods accept `jsonSerializerOptions` and `cancellationToken` parameters

#### Date/Time Formatting
- Date formatting extension changed from `ISO8601String()` to `ISO8601()`

### Legacy Support

The previous C# web client generators have been preserved for backward compatibility:
- `LegacyConvertWebApiToCSharpFile` and `LegacyCreateHttpClientRegistrations` (marked as `[Obsolete]`)
- Use `codegen csharp legacy-web-client` command to generate clients using the previous pattern that depends on `Albatross.WebClient`

### Target Frameworks
- .NET 10.0 (primary)
- .NET Standard 2.0 (core libraries)

## Version 9.0.1

### Changes
- Upgraded target framework to .NET 10.0
- Updated Microsoft.CodeAnalysis dependencies
- Enhanced C# file declaration with indexing support
- Fixed member access expression generation

## Version 8.x

### Changes
- Added Python async HTTP client generation
- Improved TypeScript type mapping
- Enhanced generic type support
- Various bug fixes and performance improvements
