from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class FromBodyParamTestClient:
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
	
	async def required_object(self, dto: MyDto) -> int:
		relativeUrl = f"required-object"
		response = await self._client.post[MyDto](relativeUrl, dto, {})
		response.raise_for_status()
	
	async def nullable_object(self, dto: MyDto|None) -> int:
		relativeUrl = f"nullable-object"
		response = await self._client.post[MyDto|None](relativeUrl, dto, {})
		response.raise_for_status()
	
	async def required_int(self, value: int) -> int:
		relativeUrl = f"required-int"
		response = await self._client.post[int](relativeUrl, value, {})
		response.raise_for_status()
	
	async def nullable_int(self, value: int|None) -> int:
		relativeUrl = f"nullable-int"
		response = await self._client.post[int|None](relativeUrl, value, {})
		response.raise_for_status()
	
	async def required_string(self, value: str) -> int:
		relativeUrl = f"required-string"
		response = await self._client.post[str](relativeUrl, value, {})
		response.raise_for_status()
	
	async def nullable_string(self, value: None|str) -> int:
		relativeUrl = f"nullable-string"
		response = await self._client.post[None|str](relativeUrl, value, {})
		response.raise_for_status()
	
	async def required_object_array(self, array: list[MyDto]) -> int:
		relativeUrl = f"required-object-array"
		response = await self._client.post[list[MyDto]](relativeUrl, array, {})
		response.raise_for_status()
	
	async def nullable_object_array(self, array: list[MyDto|None]) -> int:
		relativeUrl = f"nullable-object-array"
		response = await self._client.post[list[MyDto|None]](relativeUrl, array, {})
		response.raise_for_status()
	

