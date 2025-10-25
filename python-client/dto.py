from dataclasses import dataclass

@dataclass()
class AbsUrlRedirectTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class ArrayParamTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class CancellationTokenTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class ControllerRouteTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class CustomJsonSettingsController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class DuplicateNameTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class FilteredMethodController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class FromBodyParamTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class FromHeaderParamTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class Context:
	id: None|str
	type: None|str

@dataclass()
class FromQueryParamTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class FromRouteParamTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class HttpMethodTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class InterfaceAndAbstractClassTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class NullableParamTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class NullableReturnTypeTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class ObsoleteController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class PartiallyObsoleteController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class OmittedConstructorController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class RedirectTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class RequiredParamTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class RequiredReturnTypeTestController:
	http_context: HttpContext
	request: HttpRequest
	response: HttpResponse
	route_data: RouteData
	model_state: list[KeyValuePair[str, ModelStateEntry|None]]
	controller_context: ControllerContext
	metadata_provider: IModelMetadataProvider
	model_binder_factory: IModelBinderFactory
	url: IUrlHelper
	object_validator: IObjectModelValidator
	problem_details_factory: ProblemDetailsFactory
	user: ClaimsPrincipal
	empty: EmptyResult

@dataclass()
class MyStartup:
	configuration: IConfiguration
	authentication_settings: AuthenticationSettings
	open_api: bool
	web_api: bool
	spa: bool
	log_usage: bool
	plain_text_formatter: bool
	global_exception_handler: IGlobalExceptionHandler

