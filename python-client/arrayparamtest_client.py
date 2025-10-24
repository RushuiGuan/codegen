
class ArrayParamTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def array_string_param(array: list[str]) -> str:
		relativeUrl = f"array-string-param"
		result = this.doGetStringAsync(relativeUrl, { "a": array })
		return await xx(result);
	
	AsyncModifier { Name = async } def array_value_type(array: list[int]) -> str:
		relativeUrl = f"array-value-type"
		result = this.doGetStringAsync(relativeUrl, { "a": array })
		return await xx(result);
	
	AsyncModifier { Name = async } def collection_string_param(collection: list[str]) -> str:
		relativeUrl = f"collection-string-param"
		result = this.doGetStringAsync(relativeUrl, { "c": collection })
		return await xx(result);
	
	AsyncModifier { Name = async } def collection_value_type(collection: list[int]) -> str:
		relativeUrl = f"collection-value-type"
		result = this.doGetStringAsync(relativeUrl, { "c": collection })
		return await xx(result);
	

