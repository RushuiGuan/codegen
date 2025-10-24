from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class FilteredMethodClient:
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
	
	async def filtered_by_all():
		relativeUrl = f"all"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def filtered_by_none():
		relativeUrl = f"none"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def filtered_by_c_sharp():
		relativeUrl = f"csharp"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def filtered_by_c_sharp2():
		relativeUrl = f"csharp2"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def included_by_c_sharp():
		relativeUrl = f"include-this-method"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def filtered_by_type_script():
		relativeUrl = f"typescript"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	

