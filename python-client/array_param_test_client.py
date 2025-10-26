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
	
	async def __aexit__(self, exc_type, exc_value, traceback):
		await self.close()
	
	async def array_string_param(self, array: list[str]) -> str:
		relative_url = f"array-string-param"
		params = { "a": array }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def array_value_type(self, array: list[int]) -> str:
		relative_url = f"array-value-type"
		params = { "a": array }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def collection_string_param(self, collection: list[str]) -> str:
		relative_url = f"collection-string-param"
		params = { "c": collection }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def collection_value_type(self, collection: list[int]) -> str:
		relative_url = f"collection-value-type"
		params = { "c": collection }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	

