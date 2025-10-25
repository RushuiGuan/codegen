from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class ArrayParamTestClient:
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
	
	async def array_string_param(self, array: list[str]) -> str:
		relativeUrl = f"array-string-param"
		response = await self._client.get(relativeUrl, { "a": array })
		response.raise_for_status()
	
	async def array_value_type(self, array: list[int]) -> str:
		relativeUrl = f"array-value-type"
		response = await self._client.get(relativeUrl, { "a": array })
		response.raise_for_status()
	
	async def collection_string_param(self, collection: list[str]) -> str:
		relativeUrl = f"collection-string-param"
		response = await self._client.get(relativeUrl, { "c": collection })
		response.raise_for_status()
	
	async def collection_value_type(self, collection: list[int]) -> str:
		relativeUrl = f"collection-value-type"
		response = await self._client.get(relativeUrl, { "c": collection })
		response.raise_for_status()
	

