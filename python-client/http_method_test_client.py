# @generated

from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class HttpMethodTestClient:
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
	
	async def delete(self):
		relative_url = ""
		response = self._client.delete(relative_url)
		response.raise_for_status()
	
	async def post(self):
		relative_url = ""
		response = self._client.post[str](relative_url, "")
		response.raise_for_status()
	
	async def patch(self):
		relative_url = ""
		response = self._client.patch[str](relative_url, "")
		response.raise_for_status()
	
	async def get(self) -> int:
		relative_url = ""
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def put(self):
		relative_url = ""
		response = self._client.put[str](relative_url, "")
		response.raise_for_status()
	

