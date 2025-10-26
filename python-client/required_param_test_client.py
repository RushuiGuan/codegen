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
	
	async def __aexit__(self, exc_type, exc_value, traceback):
		await self.close()
	
	async def explicit_string_param(self, text: str) -> str:
		relative_url = f"explicit-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def implicit_string_param(self, text: str) -> str:
		relative_url = f"implicit-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_string_param(self, text: str) -> str:
		relative_url = f"required-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_value_type(self, id: int) -> str:
		relative_url = f"required-value-type"
		params = { "id": id }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_only(self, date: date) -> str:
		relative_url = f"required-date-only"
		params = { "date": date.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time(self, date: datetime) -> str:
		relative_url = f"required-datetime"
		params = { "date": date.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_as_date_only(self, date: datetime) -> str:
		relative_url = f"requried-datetime-as-dateonly"
		params = { "date": date.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_post_param(self, dto: MyDto):
		relative_url = f"required-post-param"
		params = {}
		response = await self._client.post[MyDto](relative_url, dto, params = params)
		response.raise_for_status()
	
	async def required_string_array(self, values: list[str]) -> str:
		relative_url = f"required-string-array"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_string_collection(self, values: list[str]) -> str:
		relative_url = f"required-string-collection"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_value_type_array(self, values: list[int]) -> str:
		relative_url = f"required-value-type-array"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_value_type_collection(self, values: list[int]) -> str:
		relative_url = f"required-value-type-collection"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_only_collection(self, dates: list[date]) -> str:
		relative_url = f"required-date-only-collection"
		params = { "dates": dates.map() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_only_array(self, dates: list[date]) -> str:
		relative_url = f"required-date-only-array"
		params = { "dates": dates.map() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_collection(self, dates: list[datetime]) -> str:
		relative_url = f"required-datetime-collection"
		params = { "dates": dates.map() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_array(self, dates: list[datetime]) -> str:
		relative_url = f"required-datetime-array"
		params = { "dates": dates.map() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_as_date_only_collection(self, dates: list[datetime]) -> str:
		relative_url = f"required-datetime-as-dateonly-collection"
		params = { "dates": dates.map() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_as_date_only_array(self, dates: list[datetime]) -> str:
		relative_url = f"required-datetime-as-dateonly-array"
		params = { "dates": dates.map() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	

