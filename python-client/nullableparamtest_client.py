from datetime import date

class NullableParamTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def nullable_string_param(text: None|str) -> str:
		relativeUrl = f"nullable-string-param"
		result = this.doGetStringAsync(relativeUrl, { "text": text })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_value_type(id: int|None) -> str:
		relativeUrl = f"nullable-value-type"
		result = this.doGetStringAsync(relativeUrl, { "id": id })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_date_only(date: date|None) -> str:
		relativeUrl = f"nullable-date-only"
		result = this.doGetStringAsync(relativeUrl, { "date": date })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_post_param(dto: MyDto|None):
		relativeUrl = f"nullable-post-param"
		result = this.doPostAsync[None, MyDto|None](relativeUrl, dto, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_string_array(values: list[None|str]) -> str:
		relativeUrl = f"nullable-string-array"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_string_collection(values: list[None|str]) -> str:
		relativeUrl = f"nullable-string-collection"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_value_type_array(values: list[int|None]) -> str:
		relativeUrl = f"nullable-value-type-array"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_value_type_collection(values: list[int|None]) -> str:
		relativeUrl = f"nullable-value-type-collection"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_date_only_collection(dates: list[date|None]) -> str:
		relativeUrl = f"nullable-date-only-collection"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates })
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_date_only_array(dates: list[date|None]) -> str:
		relativeUrl = f"nullable-date-only-array"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates })
		return await xx(result);
	

