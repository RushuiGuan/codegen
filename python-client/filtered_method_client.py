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
	
	async def __aexit__(self, exc_type, exc_value, traceback):
		await self.close()
	
	async def filtered_by_none(self):
		relative_url = f"none"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def filtered_by_c_sharp(self):
		relative_url = f"csharp"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def filtered_by_c_sharp2(self):
		relative_url = f"csharp2"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def included_by_c_sharp(self):
		relative_url = f"include-this-method"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def filtered_by_type_script(self):
		relative_url = f"typescript"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	

