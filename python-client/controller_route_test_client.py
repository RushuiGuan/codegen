from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class ControllerRouteTestClient:
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
	
	async def post(self):
		relativeUrl = f""
		response = await self._client.post[str](relativeUrl, "", {})
		response.raise_for_status()
	

