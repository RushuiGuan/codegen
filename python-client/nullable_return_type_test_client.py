# @generated

from datetime import datetime
from dto import MyDto
from httpx import AsyncClient, Auth
from pydantic import TypeAdapter
from typing import Self

class NullableReturnTypeTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/nullable-return-type"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def get_string(self) -> str:
		relative_url = "string"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_async_string(self) -> str:
		relative_url = "async-string"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_action_result_string(self) -> str:
		relative_url = "action-result-string"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_async_action_result_string(self) -> str:
		relative_url = "async-action-result-string"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_int(self) -> int | None:
		relative_url = "int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_async_int(self) -> int | None:
		relative_url = "async-int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_action_result_int(self) -> int | None:
		relative_url = "action-result-int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_async_action_result_int(self) -> int | None:
		relative_url = "async-action-result-int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_date_time(self) -> datetime | None:
		relative_url = "datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_async_date_time(self) -> datetime | None:
		relative_url = "async-datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_action_result_date_time(self) -> datetime | None:
		relative_url = "action-result-datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_async_action_result_date_time(self) -> datetime | None:
		relative_url = "async-action-result-datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_my_dto(self) -> MyDto | None:
		relative_url = "object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def get_async_my_dto(self) -> MyDto | None:
		relative_url = "async-object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def action_result_object(self) -> MyDto | None:
		relative_url = "action-result-object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def async_action_result_object(self) -> MyDto | None:
		relative_url = "async-action-result-object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def get_my_dto_nullable_array(self) -> list[MyDto | None]:
		relative_url = "nullable-array-return-type"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto | None]).validate_python(response.json())
	
	async def get_my_dto_collection(self) -> list[MyDto | None]:
		relative_url = "nullable-collection-return-type"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto | None]).validate_python(response.json())
	

