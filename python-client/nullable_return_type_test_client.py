# @generated

from datetime import datetime
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class NullableReturnTypeTestClient:
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
	
	async def get_string(self) -> str:
		relative_url = "string"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_string(self) -> str:
		relative_url = "async-string"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_action_result_string(self) -> str:
		relative_url = "action-result-string"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_action_result_string(self) -> str:
		relative_url = "async-action-result-string"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_int(self) -> int | None:
		relative_url = "int"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_int(self) -> int | None:
		relative_url = "async-int"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_action_result_int(self) -> int | None:
		relative_url = "action-result-int"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_action_result_int(self) -> int | None:
		relative_url = "async-action-result-int"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_date_time(self) -> datetime | None:
		relative_url = "datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_date_time(self) -> datetime | None:
		relative_url = "async-datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_action_result_date_time(self) -> datetime | None:
		relative_url = "action-result-datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_action_result_date_time(self) -> datetime | None:
		relative_url = "async-action-result-datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_my_dto(self) -> MyDto | None:
		relative_url = "object"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_my_dto(self) -> MyDto | None:
		relative_url = "async-object"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def action_result_object(self) -> MyDto | None:
		relative_url = "action-result-object"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def async_action_result_object(self) -> MyDto | None:
		relative_url = "async-action-result-object"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_my_dto_nullable_array(self) -> list[MyDto | None]:
		relative_url = "nullable-array-return-type"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_my_dto_collection(self) -> list[MyDto | None]:
		relative_url = "nullable-collection-return-type"
		response = self._client.get(relative_url)
		response.raise_for_status()
	

