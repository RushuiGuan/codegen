# @generated

from datetime import datetime, date, time
from dto import MyDto, MyEnum
from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class RequiredReturnTypeTestClient:
	_client: Session
	_base_url: str
	def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
		self._base_url = f"{base_url.rstrip('/')}/api/required-return-type"
		self._client = Session()
		self._client.auth = auth
	
	def close(self) -> None:
		self._client.close()
	
	def __enter__(self) -> Self:
		return self
	
	def __exit__(self, exc_type, exc_value, traceback) -> None:
		self.close()
	
	def get(self) -> None:
		request_url = f"{self._base_url}/void"
		response = self._client.get(request_url)
		response.raise_for_status()
	
	def get_async(self) -> None:
		request_url = f"{self._base_url}/async-task"
		response = self._client.get(request_url)
		response.raise_for_status()
	
	def get_action_result(self) -> None:
		request_url = f"{self._base_url}/action-result"
		response = self._client.get(request_url)
		response.raise_for_status()
	
	def get_async_action_result(self) -> None:
		request_url = f"{self._base_url}/async-action-result"
		response = self._client.get(request_url)
		response.raise_for_status()
	
	def get_string(self) -> str:
		request_url = f"{self._base_url}/string"
		response = self._client.get(request_url)
		response.raise_for_status()
		return response.text
	
	def get_async_string(self) -> str:
		request_url = f"{self._base_url}/async-string"
		response = self._client.get(request_url)
		response.raise_for_status()
		return response.text
	
	def get_action_result_string(self) -> str:
		request_url = f"{self._base_url}/action-result-string"
		response = self._client.get(request_url)
		response.raise_for_status()
		return response.text
	
	def get_async_action_result_string(self) -> str:
		request_url = f"{self._base_url}/async-action-result-string"
		response = self._client.get(request_url)
		response.raise_for_status()
		return response.text
	
	def get_int(self) -> int:
		request_url = f"{self._base_url}/int"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	def get_async_int(self) -> int:
		request_url = f"{self._base_url}/async-int"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	def get_action_result_int(self) -> int:
		request_url = f"{self._base_url}/action-result-int"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	def get_async_action_result_int(self) -> int:
		request_url = f"{self._base_url}/async-action-result-int"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(int).validate_python(response.json())
	
	def get_date_time(self) -> datetime:
		request_url = f"{self._base_url}/datetime"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	def get_async_date_time(self) -> datetime:
		request_url = f"{self._base_url}/async-datetime"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	def get_action_result_date_time(self) -> datetime:
		request_url = f"{self._base_url}/action-result-datetime"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	def get_async_action_result_date_time(self) -> datetime:
		request_url = f"{self._base_url}/async-action-result-datetime"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	def get_date_only(self) -> date:
		request_url = f"{self._base_url}/dateonly"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(date).validate_python(response.json())
	
	def get_date_time_offset(self) -> datetime:
		request_url = f"{self._base_url}/datetimeoffset"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(datetime).validate_python(response.json())
	
	def get_time_only(self) -> time:
		request_url = f"{self._base_url}/timeonly"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(time).validate_python(response.json())
	
	def get_my_dto(self) -> MyDto:
		request_url = f"{self._base_url}/object"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	def get_async_my_dto(self) -> MyDto:
		request_url = f"{self._base_url}/async-object"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	def action_result_object(self) -> MyDto:
		request_url = f"{self._base_url}/action-result-object"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	def async_action_result_object(self) -> MyDto:
		request_url = f"{self._base_url}/async-action-result-object"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(MyDto).validate_python(response.json())
	
	def get_my_dto_array(self) -> list[MyDto]:
		request_url = f"{self._base_url}/array-return-type"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto]).validate_python(response.json())
	
	def get_my_dto_collection(self) -> list[MyDto]:
		request_url = f"{self._base_url}/collection-return-type"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto]).validate_python(response.json())
	
	def get_my_dto_collection_async(self) -> list[MyDto]:
		request_url = f"{self._base_url}/async-collection-return-type"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(list[MyDto]).validate_python(response.json())
	
	def required_enum(self) -> MyEnum:
		request_url = f"{self._base_url}/required-enum"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(MyEnum).validate_python(response.json())
	
	def required_enum_array(self) -> list[MyEnum]:
		request_url = f"{self._base_url}/required-enum-array"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(list[MyEnum]).validate_python(response.json())
	
	def get_dynamic(self) -> dynamic:
		request_url = f"{self._base_url}/dynamic"
		response = self._client.get(request_url)
		response.raise_for_status()
		return TypeAdapter(dynamic).validate_python(response.json())
	

