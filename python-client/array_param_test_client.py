from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class ArrayParamTestClient:
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
	
	async def array_string_param(array: list[str]) -> str:
		relativeUrl = f"array-string-param"
		result = this.doGetStringAsync(relativeUrl, { "a": array })
		return await xx(result)
	
	async def array_value_type(array: list[int]) -> str:
		relativeUrl = f"array-value-type"
		result = this.doGetStringAsync(relativeUrl, { "a": array })
		return await xx(result)
	
	async def collection_string_param(collection: list[str]) -> str:
		relativeUrl = f"collection-string-param"
		result = this.doGetStringAsync(relativeUrl, { "c": collection })
		return await xx(result)
	
	async def collection_value_type(collection: list[int]) -> str:
		relativeUrl = f"collection-value-type"
		result = this.doGetStringAsync(relativeUrl, { "c": collection })
		return await xx(result)
	

