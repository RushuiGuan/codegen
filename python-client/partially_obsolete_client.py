from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class PartiallyObsoleteClient:
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
	
	async def get() -> str:
		relativeUrl = f"get"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result)
	

