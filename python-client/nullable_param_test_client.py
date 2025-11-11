# @generated

from datetime import date
from dto import MyDto
from httpx import AsyncClient, Auth
from pydantic import TypeAdapter
from typing import Self

class NullableParamTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/nullable-param-test"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def nullable_string_param(self, text: str | None) -> str:
		relative_url = "nullable-string-param"
		params = { "text": text }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_value_type(self, id: int | None) -> str:
		relative_url = "nullable-value-type"
		params = { "id": id }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_date_only(self, date: date | None) -> str:
		relative_url = "nullable-date-only"
		params = { "date": date.isoformat() if date is not None else None }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_post_param(self, dto: MyDto | None) -> None:
		relative_url = "nullable-post-param"
		response = await self._client.post(relative_url, json = TypeAdapter(MyDto | None).dump_python(dto, mode = "json"))
		response.raise_for_status()
	
	async def nullable_string_array(self, values: list[str | None]) -> str:
		relative_url = "nullable-string-array"
		params = { "values": [x if x is not None else "" for x in values] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_string_collection(self, values: list[str | None]) -> str:
		relative_url = "nullable-string-collection"
		params = { "values": [x if x is not None else "" for x in values] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_value_type_array(self, values: list[int | None]) -> str:
		relative_url = "nullable-value-type-array"
		params = { "values": [x if x is not None else "" for x in values] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_value_type_collection(self, values: list[int | None]) -> str:
		relative_url = "nullable-value-type-collection"
		params = { "values": [x if x is not None else "" for x in values] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_date_only_collection(self, dates: list[date | None]) -> str:
		relative_url = "nullable-date-only-collection"
		params = { "dates": [x.isoformat() if x is not None else "" for x in dates] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	
	async def nullable_date_only_array(self, dates: list[date | None]) -> str:
		relative_url = "nullable-date-only-array"
		params = { "dates": [x.isoformat() if x is not None else "" for x in dates] }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return response.text
	

