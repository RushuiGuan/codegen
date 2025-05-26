# 8.0.2
* Omit any controller method parameters that are annotated with `[FromHeader]` attribute.  Those parameters should be setup as part of the proxy registration.
# 7.6.0
* Add the settings to omit constructor for the csharp proxy
* Add the settings to use a custom JsonSettings instance.
* Combine the properties of the `ApiControllerConversionSettings` into `CSharpWebClientSettings`
