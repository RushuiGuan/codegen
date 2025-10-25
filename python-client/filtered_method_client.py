from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class FilteredMethodClient:
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
	
	async def filtered_by_none(self):
		relativeUrl = f"none"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def filtered_by_c_sharp(self):
		relativeUrl = f"csharp"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def filtered_by_c_sharp2(self):
		relativeUrl = f"csharp2"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def included_by_c_sharp(self):
		relativeUrl = f"include-this-method"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def filtered_by_type_script(self):
		relativeUrl = f"typescript"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	

