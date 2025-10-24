from datetime import datetime, date, time

class RequiredReturnTypeTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def get():
		relativeUrl = f"void"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async():
		relativeUrl = f"async-task"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_action_result():
		relativeUrl = f"action-result"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_action_result():
		relativeUrl = f"async-action-result"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
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
	
	AsyncModifier { Name = async } def get_int() -> int:
		relativeUrl = f"int"
		result = this.doGetAsync[int](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_int() -> int:
		relativeUrl = f"async-int"
		result = this.doGetAsync[int](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_action_result_int() -> int:
		relativeUrl = f"action-result-int"
		result = this.doGetAsync[int](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_action_result_int() -> int:
		relativeUrl = f"async-action-result-int"
		result = this.doGetAsync[int](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_date_time() -> datetime:
		relativeUrl = f"datetime"
		result = this.doGetAsync[datetime](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_date_time() -> datetime:
		relativeUrl = f"async-datetime"
		result = this.doGetAsync[datetime](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_action_result_date_time() -> datetime:
		relativeUrl = f"action-result-datetime"
		result = this.doGetAsync[datetime](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_action_result_date_time() -> datetime:
		relativeUrl = f"async-action-result-datetime"
		result = this.doGetAsync[datetime](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_date_only() -> date:
		relativeUrl = f"dateonly"
		result = this.doGetAsync[date](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_date_time_offset() -> datetime:
		relativeUrl = f"datetimeoffset"
		result = this.doGetAsync[datetime](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_time_only() -> time:
		relativeUrl = f"timeonly"
		result = this.doGetAsync[time](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_my_dto() -> MyDto:
		relativeUrl = f"object"
		result = this.doGetAsync[MyDto](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_async_my_dto() -> MyDto:
		relativeUrl = f"async-object"
		result = this.doGetAsync[MyDto](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def action_result_object() -> MyDto:
		relativeUrl = f"action-result-object"
		result = this.doGetAsync[MyDto](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def async_action_result_object() -> MyDto:
		relativeUrl = f"async-action-result-object"
		result = this.doGetAsync[MyDto](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_my_dto_array() -> list[MyDto]:
		relativeUrl = f"array-return-type"
		result = this.doGetAsync[list[MyDto]](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_my_dto_collection() -> list[MyDto]:
		relativeUrl = f"collection-return-type"
		result = this.doGetAsync[list[MyDto]](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get_my_dto_collection_async() -> list[MyDto]:
		relativeUrl = f"async-collection-return-type"
		result = this.doGetAsync[list[MyDto]](relativeUrl, {})
		return await xx(result);
	

