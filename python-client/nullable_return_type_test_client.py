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
	
	async def __aexit__(self):
		await self.close()
	
	async def get_string(self) -> str:
		relativeUrl = f"string"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_string(self) -> str:
		relativeUrl = f"async-string"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_action_result_string(self) -> str:
		relativeUrl = f"action-result-string"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_action_result_string(self) -> str:
		relativeUrl = f"async-action-result-string"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_int(self) -> int|None:
		relativeUrl = f"int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_int(self) -> int|None:
		relativeUrl = f"async-int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_action_result_int(self) -> int|None:
		relativeUrl = f"action-result-int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_action_result_int(self) -> int|None:
		relativeUrl = f"async-action-result-int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_date_time(self) -> datetime|None:
		relativeUrl = f"datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_date_time(self) -> datetime|None:
		relativeUrl = f"async-datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_action_result_date_time(self) -> datetime|None:
		relativeUrl = f"action-result-datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_action_result_date_time(self) -> datetime|None:
		relativeUrl = f"async-action-result-datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_my_dto(self) -> MyDto|None:
		relativeUrl = f"object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_my_dto(self) -> MyDto|None:
		relativeUrl = f"async-object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def action_result_object(self) -> MyDto|None:
		relativeUrl = f"action-result-object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def async_action_result_object(self) -> MyDto|None:
		relativeUrl = f"async-action-result-object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_my_dto_nullable_array(self) -> list[MyDto|None]:
		relativeUrl = f"nullable-array-return-type"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_my_dto_collection(self) -> list[MyDto|None]:
		relativeUrl = f"nullable-collection-return-type"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	

