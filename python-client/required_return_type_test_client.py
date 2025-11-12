# @generated

from datetime import datetime, date, time
from dto import MyDto, MyEnum
from httpx import AsyncClient, Auth
from pydantic import TypeAdapter
from typing import Self

class RequiredReturnTypeTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/required-return-type"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def get(self) -> None:
		relative_url = "void"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async(self) -> None:
		relative_url = "async-task"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_action_result(self) -> None:
		relative_url = "action-result"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_action_result(self) -> None:
		relative_url = "async-action-result"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
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
	
	async def get_int(self) -> int:
		relative_url = "int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def get_async_int(self) -> int:
		relative_url = "async-int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def get_action_result_int(self) -> int:
		relative_url = "action-result-int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def get_async_action_result_int(self) -> int:
		relative_url = "async-action-result-int"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def get_date_time(self) -> datetime:
		relative_url = "datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	async def get_async_date_time(self) -> datetime:
		relative_url = "async-datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	async def get_action_result_date_time(self) -> datetime:
		relative_url = "action-result-datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	async def get_async_action_result_date_time(self) -> datetime:
		relative_url = "async-action-result-datetime"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	async def get_date_only(self) -> date:
		relative_url = "dateonly"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(date).validate_python(response.json())
	
	async def get_date_time_offset(self) -> datetime:
		relative_url = "datetimeoffset"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	async def get_time_only(self) -> time:
		relative_url = "timeonly"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(time).validate_python(response.json())
	
	async def get_my_dto(self) -> MyDto:
		relative_url = "object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	async def get_async_my_dto(self) -> MyDto:
		relative_url = "async-object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	async def action_result_object(self) -> MyDto:
		relative_url = "action-result-object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	async def async_action_result_object(self) -> MyDto:
		relative_url = "async-action-result-object"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	async def get_my_dto_array(self) -> list[MyDto]:
		relative_url = "array-return-type"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto]).validate_python(response.json())
	
	async def get_my_dto_collection(self) -> list[MyDto]:
		relative_url = "collection-return-type"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto]).validate_python(response.json())
	
	async def get_my_dto_collection_async(self) -> list[MyDto]:
		relative_url = "async-collection-return-type"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto]).validate_python(response.json())
	
	async def required_enum(self) -> MyEnum:
		relative_url = "required-enum"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(MyEnum).validate_python(response.json())
	
	async def required_enum_array(self) -> list[MyEnum]:
		relative_url = "required-enum-array"
		response = await self._client.get(relative_url)
		response.raise_for_status()
		return TypeAdapter(list[MyEnum]).validate_python(response.json())
	

