from datetime import datetime, date, time
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class RequiredReturnTypeTestClient:
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
	
	async def get(self):
		relativeUrl = f"void"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async(self):
		relativeUrl = f"async-task"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_action_result(self):
		relativeUrl = f"action-result"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_action_result(self):
		relativeUrl = f"async-action-result"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
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
	
	async def get_int(self) -> int:
		relativeUrl = f"int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_int(self) -> int:
		relativeUrl = f"async-int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_action_result_int(self) -> int:
		relativeUrl = f"action-result-int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_action_result_int(self) -> int:
		relativeUrl = f"async-action-result-int"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_date_time(self) -> datetime:
		relativeUrl = f"datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_date_time(self) -> datetime:
		relativeUrl = f"async-datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_action_result_date_time(self) -> datetime:
		relativeUrl = f"action-result-datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_action_result_date_time(self) -> datetime:
		relativeUrl = f"async-action-result-datetime"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_date_only(self) -> date:
		relativeUrl = f"dateonly"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_date_time_offset(self) -> datetime:
		relativeUrl = f"datetimeoffset"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_time_only(self) -> time:
		relativeUrl = f"timeonly"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_my_dto(self) -> MyDto:
		relativeUrl = f"object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_async_my_dto(self) -> MyDto:
		relativeUrl = f"async-object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def action_result_object(self) -> MyDto:
		relativeUrl = f"action-result-object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def async_action_result_object(self) -> MyDto:
		relativeUrl = f"async-action-result-object"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_my_dto_array(self) -> list[MyDto]:
		relativeUrl = f"array-return-type"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_my_dto_collection(self) -> list[MyDto]:
		relativeUrl = f"collection-return-type"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def get_my_dto_collection_async(self) -> list[MyDto]:
		relativeUrl = f"async-collection-return-type"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	

