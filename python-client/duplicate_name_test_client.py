from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class DuplicateNameTestClient:
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
	
	async def submit(self, id: int):
		relativeUrl = f"by-id"
		response = await self._client.post[str](relativeUrl, "", { "id": id })
		response.raise_for_status()
	
	async def submit1(self, name: str):
		relativeUrl = f"by-name"
		response = await self._client.post[str](relativeUrl, "", { "name": name })
		response.raise_for_status()
	

