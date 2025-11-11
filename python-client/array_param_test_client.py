# @generated

from datetime import date, datetime
from httpx import AsyncClient, Auth
from typing import Self

class ArrayParamTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/array-param-test"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def array_string_param(self, array: list[str]) -> str:
		relative_url = "array-string-param"
		params = { "a": array }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def array_value_type(self, array: list[int]) -> str:
		relative_url = "array-value-type"
		params = { "a": array }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def collection_string_param(self, collection: list[str]) -> str:
		relative_url = "collection-string-param"
		params = { "c": collection }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def collection_value_type(self, collection: list[int]) -> str:
		relative_url = "collection-value-type"
		params = { "c": collection }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def collection_date_param(self, collection: list[date]) -> str:
		relative_url = "collection-date-param"
		params = { "c": [x.isoformat() for x in collection] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def collection_date_time_param(self, collection: list[datetime]) -> str:
		relative_url = "collection-datetime-param"
		params = { "c": [x.isoformat() for x in collection] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	

