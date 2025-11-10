# @generated

from httpx import AsyncClient, Auth
from pydantic import TypeAdapter
from typing import Self

class HttpMethodTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/http-method-test"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def delete(self) -> None:
		relative_url = ""
		response = await self._client.delete(relative_url)
		response.raise_for_status()
	
	async def post(self) -> None:
		relative_url = ""
		response = await self._client.post(relative_url)
		response.raise_for_status()
	
	async def patch(self) -> None:
		relative_url = ""
		response = await self._client.patch(relative_url)
		response.raise_for_status()
	
	async def get(self) -> int:
		relative_url = ""
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def put(self) -> None:
		relative_url = ""
		response = await self._client.put(relative_url)
		response.raise_for_status()
	

