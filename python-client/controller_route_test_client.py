# @generated

from httpx import AsyncClient, Auth
from typing import Self

class ControllerRouteTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/controllerroutetest"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def post(self) -> None:
		relative_url = ""
		response = await self._client.post(relative_url)
		response.raise_for_status()
	

