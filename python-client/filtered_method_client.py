# @generated

from httpx import AsyncClient, Auth
from typing import Self

class FilteredMethodClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/filtered-method"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def filtered_by_all(self) -> None:
		relative_url = "all"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_none(self) -> None:
		relative_url = "none"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_c_sharp(self) -> None:
		relative_url = "csharp"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_c_sharp2(self) -> None:
		relative_url = "csharp2"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def included_by_c_sharp(self) -> None:
		relative_url = "include-this-method"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def filtered_by_type_script(self) -> None:
		relative_url = "typescript"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	

