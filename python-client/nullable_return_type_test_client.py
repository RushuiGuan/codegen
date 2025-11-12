# @generated

from datetime import datetime, timezone
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
	
	async def get_string(self, text: str | None) -> str | None:
		relative_url = "string"
		params = {
			"text": text
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	async def get_async_string(self, text: str | None) -> str | None:
		relative_url = "async-string"
		params = {
			"text": text
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	async def get_action_result_string(self, text: str | None) -> str | None:
		relative_url = "action-result-string"
		params = {
			"text": text
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	async def get_async_action_result_string(self, text: str | None) -> str | None:
		relative_url = "async-action-result-string"
		params = {
			"text": text
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	async def get_int(self, n: int | None) -> int | None:
		relative_url = "int"
		params = {
			"n": n
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_async_int(self, n: int | None) -> int | None:
		relative_url = "async-int"
		params = {
			"n": n
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_action_result_int(self, n: int | None) -> int | None:
		relative_url = "action-result-int"
		params = {
			"n": n
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_async_action_result_int(self, n: int | None) -> int | None:
		relative_url = "async-action-result-int"
		params = {
			"n": n
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	async def get_date_time(self, v: datetime | None) -> datetime | None:
		relative_url = "datetime"
		params = {
			"v": v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat()
			if v is not None
			else None
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_async_date_time(self, v: datetime | None) -> datetime | None:
		relative_url = "async-datetime"
		params = {
			"v": v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat()
			if v is not None
			else None
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_action_result_date_time(self, v: datetime | None) -> datetime | None:
		relative_url = "action-result-datetime"
		params = {
			"v": (v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat())
			if v is not None
			else None
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_async_action_result_date_time(self, v: datetime | None) -> datetime | None:
		relative_url = "async-action-result-datetime"
		params = {
			"v": v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat()
			if v is not None
			else None
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	async def get_my_dto(self, value: MyDto | None) -> MyDto | None:
		relative_url = "object"
		response = await self._client.post(relative_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def get_async_my_dto(self, value: MyDto | None) -> MyDto | None:
		relative_url = "async-object"
		response = await self._client.post(relative_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def action_result_object(self, value: MyDto | None) -> MyDto | None:
		relative_url = "action-result-object"
		response = await self._client.post(relative_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def async_action_result_object(self, value: MyDto | None) -> MyDto | None:
		relative_url = "async-action-result-object"
		response = await self._client.post(relative_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	async def get_my_dto_nullable_array(self, values: list[MyDto | None]) -> list[MyDto | None]:
		relative_url = "nullable-array-return-type"
		response = await self._client.post(relative_url, json = TypeAdapter(list[MyDto | None]).dump_python(values, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(list[MyDto | None]).validate_python(response.json())
	
	async def get_my_dto_collection(self, values: list[MyDto | None]) -> list[MyDto | None]:
		relative_url = "nullable-collection-return-type"
		response = await self._client.post(relative_url, json = TypeAdapter(list[MyDto | None]).dump_python(values, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(list[MyDto | None]).validate_python(response.json())
	

