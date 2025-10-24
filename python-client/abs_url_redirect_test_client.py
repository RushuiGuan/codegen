from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class AbsUrlRedirectTestClient:
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
	
	async def get():
		relativeUrl = f"test-0"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get1():
		relativeUrl = f"test-1"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get2():
		relativeUrl = f"test-2"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get3():
		relativeUrl = f"test-3"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get4():
		relativeUrl = f"test-4"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get5():
		relativeUrl = f"test-5"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get6():
		relativeUrl = f"test-6"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get7():
		relativeUrl = f"test-7"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get8():
		relativeUrl = f"test-8"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get9():
		relativeUrl = f"test-9"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def get10() -> str:
		relativeUrl = f"test-10"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result)
	

