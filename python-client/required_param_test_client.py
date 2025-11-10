# @generated

from datetime import date, datetime
from dto import MyDto
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
		relative_url = "explicit-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def implicit_string_param(self, text: str) -> str:
		relative_url = "implicit-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_string_param(self, text: str) -> str:
		relative_url = "required-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_value_type(self, id: int) -> str:
		relative_url = "required-value-type"
		params = { "id": id }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_date_only(self, date: date) -> str:
		relative_url = "required-date-only"
		params = { "date": date.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_date_time(self, date: datetime) -> str:
		relative_url = "required-datetime"
		params = { "date": date.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_post_param(self, dto: MyDto):
		relative_url = "required-post-param"
		response = self._client.post[MyDto](relative_url, dto)
		response.raise_for_status()
	
	async def required_string_array(self, values: list[str]) -> str:
		relative_url = "required-string-array"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_string_collection(self, values: list[str]) -> str:
		relative_url = "required-string-collection"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_value_type_array(self, values: list[int]) -> str:
		relative_url = "required-value-type-array"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_value_type_collection(self, values: list[int]) -> str:
		relative_url = "required-value-type-collection"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_date_only_collection(self, dates: list[date]) -> str:
		relative_url = "required-date-only-collection"
		params = { "dates": [d.isoformat() for d in dates] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_date_only_array(self, dates: list[date]) -> str:
		relative_url = "required-date-only-array"
		params = { "dates": [d.isoformat() for d in dates] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_date_time_collection(self, dates: list[datetime]) -> str:
		relative_url = "required-datetime-collection"
		params = { "dates": [d.isoformat() for d in dates] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def required_date_time_array(self, dates: list[datetime]) -> str:
		relative_url = "required-datetime-array"
		params = { "dates": [d.isoformat() for d in dates] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	

