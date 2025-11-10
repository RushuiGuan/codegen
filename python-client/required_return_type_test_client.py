# @generated

from datetime import datetime, date, time
from dto import MyDto
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth
from pydantic import TypeAdapter

class RequiredReturnTypeTestClient:
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
	
	async def get(self):
		relative_url = "void"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async(self):
		relative_url = "async-task"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_action_result(self):
		relative_url = "action-result"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_async_action_result(self):
		relative_url = "async-action-result"
		response = self._client.get(relative_url)
		response.raise_for_status()
	
	async def get_string(self) -> str:
		relative_url = "string"
		response = self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_async_string(self) -> str:
		relative_url = "async-string"
		response = self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_action_result_string(self) -> str:
		relative_url = "action-result-string"
		response = self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_async_action_result_string(self) -> str:
		relative_url = "async-action-result-string"
		response = self._client.get(relative_url)
		response.raise_for_status()
		return response.text
	
	async def get_int(self) -> int:
		relative_url = "int"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(int)
		adapter.validate_python(response.json())
	
	async def get_async_int(self) -> int:
		relative_url = "async-int"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(int)
		adapter.validate_python(response.json())
	
	async def get_action_result_int(self) -> int:
		relative_url = "action-result-int"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(int)
		adapter.validate_python(response.json())
	
	async def get_async_action_result_int(self) -> int:
		relative_url = "async-action-result-int"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(int)
		adapter.validate_python(response.json())
	
	async def get_date_time(self) -> datetime:
		relative_url = "datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(datetime)
		adapter.validate_python(response.json())
	
	async def get_async_date_time(self) -> datetime:
		relative_url = "async-datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(datetime)
		adapter.validate_python(response.json())
	
	async def get_action_result_date_time(self) -> datetime:
		relative_url = "action-result-datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(datetime)
		adapter.validate_python(response.json())
	
	async def get_async_action_result_date_time(self) -> datetime:
		relative_url = "async-action-result-datetime"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(datetime)
		adapter.validate_python(response.json())
	
	async def get_date_only(self) -> date:
		relative_url = "dateonly"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(date)
		adapter.validate_python(response.json())
	
	async def get_date_time_offset(self) -> datetime:
		relative_url = "datetimeoffset"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(datetime)
		adapter.validate_python(response.json())
	
	async def get_time_only(self) -> time:
		relative_url = "timeonly"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(time)
		adapter.validate_python(response.json())
	
	async def get_my_dto(self) -> MyDto:
		relative_url = "object"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(MyDto)
		adapter.validate_python(response.json())
	
	async def get_async_my_dto(self) -> MyDto:
		relative_url = "async-object"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(MyDto)
		adapter.validate_python(response.json())
	
	async def action_result_object(self) -> MyDto:
		relative_url = "action-result-object"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(MyDto)
		adapter.validate_python(response.json())
	
	async def async_action_result_object(self) -> MyDto:
		relative_url = "async-action-result-object"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(MyDto)
		adapter.validate_python(response.json())
	
	async def get_my_dto_array(self) -> list[MyDto]:
		relative_url = "array-return-type"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(list[MyDto])
		adapter.validate_python(response.json())
	
	async def get_my_dto_collection(self) -> list[MyDto]:
		relative_url = "collection-return-type"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(list[MyDto])
		adapter.validate_python(response.json())
	
	async def get_my_dto_collection_async(self) -> list[MyDto]:
		relative_url = "async-collection-return-type"
		response = self._client.get(relative_url)
		response.raise_for_status()
		adapter = TypeAdapter(list[MyDto])
		adapter.validate_python(response.json())
	

