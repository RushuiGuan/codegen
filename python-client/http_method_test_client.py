from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class HttpMethodTestClient:
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
	
	async def delete():
		relativeUrl = f""
		result = this.doDeleteAsync(relativeUrl, {})
		return await xx(result)
	
	async def post():
		relativeUrl = f""
		result = this.doPostAsync[None, str](relativeUrl, "", {})
		return await xx(result)
	
	async def patch():
		relativeUrl = f""
		result = this.doPatchAsync[None, str](relativeUrl, "", {})
		return await xx(result)
	
	async def get() -> int:
		relativeUrl = f""
		result = this.doGetAsync[int](relativeUrl, {})
		return await xx(result)
	
	async def put():
		relativeUrl = f""
		result = this.doPutAsync[None, str](relativeUrl, "", {})
		return await xx(result)
	

