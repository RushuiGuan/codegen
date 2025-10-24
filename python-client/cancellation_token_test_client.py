from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class CancellationTokenTestClient:
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
	
	async def get(cancellationToken: CancellationToken) -> str:
		relativeUrl = f""
		result = this.doGetStringAsync(relativeUrl, { "cancellationToken": cancellationToken })
		return await xx(result)
	

