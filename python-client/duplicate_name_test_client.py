from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class DuplicateNameTestClient:
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
	
	async def submit(id: int):
		relativeUrl = f"by-id"
		result = this.doPostAsync[None, str](relativeUrl, "", { "id": id })
		return await xx(result)
	
	async def submit1(name: str):
		relativeUrl = f"by-name"
		result = this.doPostAsync[None, str](relativeUrl, "", { "name": name })
		return await xx(result)
	

