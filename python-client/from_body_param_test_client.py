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
	
	async def __aexit__(self, exc_type, exc_value, traceback):
		await self.close()
	
	async def required_object(self, dto: MyDto) -> int:
		relative_url = f"required-object"
		params = {}
		response = await self._client.post[MyDto](relative_url, dto, params = params)
		response.raise_for_status()
	
	async def nullable_object(self, dto: MyDto|None) -> int:
		relative_url = f"nullable-object"
		params = {}
		response = await self._client.post[MyDto|None](relative_url, dto, params = params)
		response.raise_for_status()
	
	async def required_int(self, value: int) -> int:
		relative_url = f"required-int"
		params = {}
		response = await self._client.post[int](relative_url, value, params = params)
		response.raise_for_status()
	
	async def nullable_int(self, value: int|None) -> int:
		relative_url = f"nullable-int"
		params = {}
		response = await self._client.post[int|None](relative_url, value, params = params)
		response.raise_for_status()
	
	async def required_string(self, value: str) -> int:
		relative_url = f"required-string"
		params = {}
		response = await self._client.post[str](relative_url, value, params = params)
		response.raise_for_status()
	
	async def nullable_string(self, value: None|str) -> int:
		relative_url = f"nullable-string"
		params = {}
		response = await self._client.post[None|str](relative_url, value, params = params)
		response.raise_for_status()
	
	async def required_object_array(self, array: list[MyDto]) -> int:
		relative_url = f"required-object-array"
		params = {}
		response = await self._client.post[list[MyDto]](relative_url, array, params = params)
		response.raise_for_status()
	
	async def nullable_object_array(self, array: list[MyDto|None]) -> int:
		relative_url = f"nullable-object-array"
		params = {}
		response = await self._client.post[list[MyDto|None]](relative_url, array, params = params)
		response.raise_for_status()
	

