from datetime import date, datetime
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class RequiredParamTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str):
		base_url = base_url.rstrip("/")
		self._client = AsyncClient(base_url = base_url, auth = HttpNtlmAuth(None, None))
	
	async def close(self):
		await self._client.aclose()
	
	async def __aenter__(self):
		return self
	
	async def __aexit__(self):
		await self.close()
	
	async def explicit_string_param(self, text: str) -> str:
		relativeUrl = f"explicit-string-param"
		response = await self._client.get(relativeUrl, { "text": text })
		response.raise_for_status()
	
	async def implicit_string_param(self, text: str) -> str:
		relativeUrl = f"implicit-string-param"
		response = await self._client.get(relativeUrl, { "text": text })
		response.raise_for_status()
	
	async def required_string_param(self, text: str) -> str:
		relativeUrl = f"required-string-param"
		response = await self._client.get(relativeUrl, { "text": text })
		response.raise_for_status()
	
	async def required_value_type(self, id: int) -> str:
		relativeUrl = f"required-value-type"
		response = await self._client.get(relativeUrl, { "id": id })
		response.raise_for_status()
	
	async def required_date_only(self, date: date) -> str:
		relativeUrl = f"required-date-only"
		response = await self._client.get(relativeUrl, { "date": format(date, "yyyy-MM-dd") })
		response.raise_for_status()
	
	async def required_date_time(self, date: datetime) -> str:
		relativeUrl = f"required-datetime"
		response = await self._client.get(relativeUrl, { "date": format(date, "yyyy-MM-ddTHH:mm:ssXXX") })
		response.raise_for_status()
	
	async def required_date_time_as_date_only(self, date: datetime) -> str:
		relativeUrl = f"requried-datetime-as-dateonly"
		response = await self._client.get(relativeUrl, { "date": format(date, "yyyy-MM-ddTHH:mm:ssXXX") })
		response.raise_for_status()
	
	async def required_post_param(self, dto: MyDto):
		relativeUrl = f"required-post-param"
		response = await self._client.post[MyDto](relativeUrl, dto, {})
		response.raise_for_status()
	
	async def required_string_array(self, values: list[str]) -> str:
		relativeUrl = f"required-string-array"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def required_string_collection(self, values: list[str]) -> str:
		relativeUrl = f"required-string-collection"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def required_value_type_array(self, values: list[int]) -> str:
		relativeUrl = f"required-value-type-array"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def required_value_type_collection(self, values: list[int]) -> str:
		relativeUrl = f"required-value-type-collection"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def required_date_only_collection(self, dates: list[date]) -> str:
		relativeUrl = f"required-date-only-collection"
		response = await self._client.get(relativeUrl, { "dates": dates.map() })
		response.raise_for_status()
	
	async def required_date_only_array(self, dates: list[date]) -> str:
		relativeUrl = f"required-date-only-array"
		response = await self._client.get(relativeUrl, { "dates": dates.map() })
		response.raise_for_status()
	
	async def required_date_time_collection(self, dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-collection"
		response = await self._client.get(relativeUrl, { "dates": dates.map() })
		response.raise_for_status()
	
	async def required_date_time_array(self, dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-array"
		response = await self._client.get(relativeUrl, { "dates": dates.map() })
		response.raise_for_status()
	
	async def required_date_time_as_date_only_collection(self, dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-as-dateonly-collection"
		response = await self._client.get(relativeUrl, { "dates": dates.map() })
		response.raise_for_status()
	
	async def required_date_time_as_date_only_array(self, dates: list[datetime]) -> str:
		relativeUrl = f"required-datetime-as-dateonly-array"
		response = await self._client.get(relativeUrl, { "dates": dates.map() })
		response.raise_for_status()
	

