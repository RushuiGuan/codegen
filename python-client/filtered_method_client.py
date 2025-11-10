# @generated

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
	
	async def filtered_by_all(self):
		relative_url = "all"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_none(self):
		relative_url = "none"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_c_sharp(self):
		relative_url = "csharp"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_c_sharp2(self):
		relative_url = "csharp2"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def included_by_c_sharp(self):
		relative_url = "include-this-method"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_type_script(self):
		relative_url = "typescript"
		response = self._client.get(relative_url)
		response.raise_for_status()
	

