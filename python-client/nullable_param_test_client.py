# @generated

from datetime import date
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class NullableParamTestClient:
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
	
	async def nullable_string_param(self, text: None | str) -> str:
		relative_url = "nullable-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_value_type(self, id: int | None) -> str:
		relative_url = "nullable-value-type"
		params = { "id": id }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_date_only(self, date: date | None) -> str:
		relative_url = "nullable-date-only"
		params = { "date": date }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_post_param(self, dto: MyDto | None):
		relative_url = "nullable-post-param"
		response = self._client.post[MyDto | None](relative_url, dto)
		response.raise_for_status()
	
	async def nullable_string_array(self, values: list[None | str]) -> str:
		relative_url = "nullable-string-array"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_string_collection(self, values: list[None | str]) -> str:
		relative_url = "nullable-string-collection"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_value_type_array(self, values: list[int | None]) -> str:
		relative_url = "nullable-value-type-array"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_value_type_collection(self, values: list[int | None]) -> str:
		relative_url = "nullable-value-type-collection"
		params = { "values": values }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_date_only_collection(self, dates: list[date | None]) -> str:
		relative_url = "nullable-date-only-collection"
		params = { "dates": dates }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_date_only_array(self, dates: list[date | None]) -> str:
		relative_url = "nullable-date-only-array"
		params = { "dates": dates }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	

