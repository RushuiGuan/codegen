
class FromBodyParamTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def required_object(dto: MyDto) -> int:
		relativeUrl = f"required-object"
		result = this.doPostAsync[int, MyDto](relativeUrl, dto, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_object(dto: MyDto|None) -> int:
		relativeUrl = f"nullable-object"
		result = this.doPostAsync[int, MyDto|None](relativeUrl, dto, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def required_int(value: int) -> int:
		relativeUrl = f"required-int"
		result = this.doPostAsync[int, int](relativeUrl, value, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_int(value: int|None) -> int:
		relativeUrl = f"nullable-int"
		result = this.doPostAsync[int, int|None](relativeUrl, value, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def required_string(value: str) -> int:
		relativeUrl = f"required-string"
		result = this.doPostAsync[int, str](relativeUrl, value, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_string(value: None|str) -> int:
		relativeUrl = f"nullable-string"
		result = this.doPostAsync[int, None|str](relativeUrl, value, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def required_object_array(array: list[MyDto]) -> int:
		relativeUrl = f"required-object-array"
		result = this.doPostAsync[int, list[MyDto]](relativeUrl, array, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def nullable_object_array(array: list[MyDto|None]) -> int:
		relativeUrl = f"nullable-object-array"
		result = this.doPostAsync[int, list[MyDto|None]](relativeUrl, array, {})
		return await xx(result);
	

