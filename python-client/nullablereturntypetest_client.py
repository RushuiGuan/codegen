from datetime import datetime

class NullableReturnTypeTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def get_string() -> str:
		relativeUrl = f"string"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_string() -> str:
		relativeUrl = f"async-string"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_action_result_string() -> str:
		relativeUrl = f"action-result-string"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_action_result_string() -> str:
		relativeUrl = f"async-action-result-string"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_int() -> int|None:
		relativeUrl = f"int"
		result = this.doGetAsync[int|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_int() -> int|None:
		relativeUrl = f"async-int"
		result = this.doGetAsync[int|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_action_result_int() -> int|None:
		relativeUrl = f"action-result-int"
		result = this.doGetAsync[int|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_action_result_int() -> int|None:
		relativeUrl = f"async-action-result-int"
		result = this.doGetAsync[int|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_date_time() -> datetime|None:
		relativeUrl = f"datetime"
		result = this.doGetAsync[datetime|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_date_time() -> datetime|None:
		relativeUrl = f"async-datetime"
		result = this.doGetAsync[datetime|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_action_result_date_time() -> datetime|None:
		relativeUrl = f"action-result-datetime"
		result = this.doGetAsync[datetime|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_action_result_date_time() -> datetime|None:
		relativeUrl = f"async-action-result-datetime"
		result = this.doGetAsync[datetime|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_my_dto() -> MyDto|None:
		relativeUrl = f"object"
		result = this.doGetAsync[MyDto|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_my_dto() -> MyDto|None:
		relativeUrl = f"async-object"
		result = this.doGetAsync[MyDto|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def action_result_object() -> MyDto|None:
		relativeUrl = f"action-result-object"
		result = this.doGetAsync[MyDto|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def async_action_result_object() -> MyDto|None:
		relativeUrl = f"async-action-result-object"
		result = this.doGetAsync[MyDto|None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_my_dto_nullable_array() -> list[MyDto|None]:
		relativeUrl = f"nullable-array-return-type"
		result = this.doGetAsync[list[MyDto|None]](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_my_dto_collection() -> list[MyDto|None]:
		relativeUrl = f"nullable-collection-return-type"
		result = this.doGetAsync[list[MyDto|None]](relativeUrl, {})
		return await xx(result);
	

