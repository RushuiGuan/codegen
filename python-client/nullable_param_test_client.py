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
	
	async def __aexit__(self):
		await self.close()
	
	async def nullable_string_param(self, text: None|str) -> str:
		relativeUrl = f"nullable-string-param"
		response = await self._client.get(relativeUrl, { "text": text })
		response.raise_for_status()
	
	async def nullable_value_type(self, id: int|None) -> str:
		relativeUrl = f"nullable-value-type"
		response = await self._client.get(relativeUrl, { "id": id })
		response.raise_for_status()
	
	async def nullable_date_only(self, date: date|None) -> str:
		relativeUrl = f"nullable-date-only"
		response = await self._client.get(relativeUrl, { "date": date })
		response.raise_for_status()
	
	async def nullable_post_param(self, dto: MyDto|None):
		relativeUrl = f"nullable-post-param"
		response = await self._client.post[MyDto|None](relativeUrl, dto, {})
		response.raise_for_status()
	
	async def nullable_string_array(self, values: list[None|str]) -> str:
		relativeUrl = f"nullable-string-array"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def nullable_string_collection(self, values: list[None|str]) -> str:
		relativeUrl = f"nullable-string-collection"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def nullable_value_type_array(self, values: list[int|None]) -> str:
		relativeUrl = f"nullable-value-type-array"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def nullable_value_type_collection(self, values: list[int|None]) -> str:
		relativeUrl = f"nullable-value-type-collection"
		response = await self._client.get(relativeUrl, { "values": values })
		response.raise_for_status()
	
	async def nullable_date_only_collection(self, dates: list[date|None]) -> str:
		relativeUrl = f"nullable-date-only-collection"
		response = await self._client.get(relativeUrl, { "dates": dates })
		response.raise_for_status()
	
	async def nullable_date_only_array(self, dates: list[date|None]) -> str:
		relativeUrl = f"nullable-date-only-array"
		response = await self._client.get(relativeUrl, { "dates": dates })
		response.raise_for_status()
	

