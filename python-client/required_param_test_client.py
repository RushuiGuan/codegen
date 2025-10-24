from datetime import date, datetime
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class RequiredParamTestClient:
	base_url: str
	_client: AsyncClient
	def __init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
		self._client = AsyncClient(base_url = self.base_url, auth = HttpNtlmAuth(None, None))
	
	async def close(self):
		await self._client.aclose()
	
	async def __aenter__(self):
		return self
	
	async def __aexit__(self):
		await self.close()
	
	async def explicit_string_param(text: str) -> str:
		relativeUrl = f"explicit-string-param"
		result = this.doGetStringAsync(relativeUrl, { "text": text })
		return await xx(result)
	
	async def implicit_string_param(text: str) -> str:
		relativeUrl = f"implicit-string-param"
		result = this.doGetStringAsync(relativeUrl, { "text": text })
		return await xx(result)
	
	async def required_string_param(text: str) -> str:
		relativeUrl = f"required-string-param"
		result = this.doGetStringAsync(relativeUrl, { "text": text })
		return await xx(result)
	
	async def required_value_type(id: int) -> str:
		relativeUrl = f"required-value-type"
		result = this.doGetStringAsync(relativeUrl, { "id": id })
		return await xx(result)
	
	async def required_date_only(date: date) -> str:
		relativeUrl = f"required-date-only"
		result = this.doGetStringAsync(relativeUrl, { "date": format(date, "yyyy-MM-dd") })
		return await xx(result)
	
	async def required_date_time(date: datetime) -> str:
		relativeUrl = f"required-datetime"
		result = this.doGetStringAsync(relativeUrl, { "date": format(date, "yyyy-MM-ddTHH:mm:ssXXX") })
		return await xx(result)
	
	async def required_date_time_as_date_only(date: datetime) -> str:
		relativeUrl = f"requried-datetime-as-dateonly"
		result = this.doGetStringAsync(relativeUrl, { "date": format(date, "yyyy-MM-ddTHH:mm:ssXXX") })
		return await xx(result)
	
	async def required_post_param(dto: MyDto):
		relativeUrl = f"required-post-param"
		result = this.doPostAsync[None, MyDto](relativeUrl, dto, {})
		return await xx(result)
	
	async def required_string_array(values: list[str]) -> str:
		relativeUrl = f"required-string-array"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result)
	
	async def required_string_collection(values: list[str]) -> str:
		relativeUrl = f"required-string-collection"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result)
	
	async def required_value_type_array(values: list[int]) -> str:
		relativeUrl = f"required-value-type-array"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result)
	
	async def required_value_type_collection(values: list[int]) -> str:
		relativeUrl = f"required-value-type-collection"
		result = this.doGetStringAsync(relativeUrl, { "values": values })
		return await xx(result)
	
	async def required_date_only_collection(dates: list[date]) -> str:
		relativeUrl = f"required-date-only-collection"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates.map() })
		return await xx(result)
	
	async def required_date_only_array(dates: list[date]) -> str:
		relativeUrl = f"required-date-only-array"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates.map() })
		return await xx(result)
	
	async def required_date_time_collection(dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-collection"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates.map() })
		return await xx(result)
	
	async def required_date_time_array(dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-array"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates.map() })
		return await xx(result)
	
	async def required_date_time_as_date_only_collection(dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-as-dateonly-collection"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates.map() })
		return await xx(result)
	
	async def required_date_time_as_date_only_array(dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-as-dateonly-array"
		result = this.doGetStringAsync(relativeUrl, { "dates": dates.map() })
		return await xx(result)
	

