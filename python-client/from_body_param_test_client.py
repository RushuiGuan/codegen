# @generated

from dto import MyDto
from httpx import AsyncClient, Auth
from pydantic import TypeAdapter
from typing import Self

class FromBodyParamTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/from-body-param-test"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def required_object(self, dto: MyDto) -> int:
		relative_url = "required-object"
		response = await self._client.post(relative_url, json = TypeAdapter(MyDto).dump_python(dto, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def nullable_object(self, dto: MyDto | None) -> int:
		relative_url = "nullable-object"
		response = await self._client.post(relative_url, json = TypeAdapter(MyDto | None).dump_python(dto, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def required_int(self, value: int) -> int:
		relative_url = "required-int"
		response = await self._client.post(relative_url, json = TypeAdapter(int).dump_python(value, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def nullable_int(self, value: int | None) -> int:
		relative_url = "nullable-int"
		response = await self._client.post(relative_url, json = TypeAdapter(int | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def required_string(self, value: str) -> int:
		relative_url = "required-string"
		response = await self._client.post(relative_url, headers = { "Content-Type": "text/plain" }, content = value)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def nullable_string(self, value: None | str) -> int:
		relative_url = "nullable-string"
		response = await self._client.post(relative_url, headers = { "Content-Type": "text/plain" }, content = value if value is not None else "")
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def required_object_array(self, array: list[MyDto]) -> int:
		relative_url = "required-object-array"
		response = await self._client.post(relative_url, json = TypeAdapter(list[MyDto]).dump_python(array, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	async def nullable_object_array(self, array: list[MyDto | None]) -> int:
		relative_url = "nullable-object-array"
		response = await self._client.post(relative_url, json = TypeAdapter(list[MyDto | None]).dump_python(array, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	

