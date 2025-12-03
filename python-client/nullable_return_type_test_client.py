# @generated

from datetime import datetime, timezone
from dto import MyDto
from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class NullableReturnTypeTestClient:
	_client: Session
	_base_url: str
	def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
		self._base_url = f"{base_url.rstrip('/')}/api/nullable-return-type"
		self._client = Session()
		self._client.auth = auth
	
	def close(self) -> None:
		self._client.close()
	
	def __enter__(self) -> Self:
		return self
	
	def __exit__(self, exc_type, exc_value, traceback) -> None:
		self.close()
	
	def get_string(self, text: str | None) -> str | None:
		request_url = f"{self._base_url}/string"
		params = {
			"text": text
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	def get_async_string(self, text: str | None) -> str | None:
		request_url = f"{self._base_url}/async-string"
		params = {
			"text": text
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	def get_action_result_string(self, text: str | None) -> str | None:
		request_url = f"{self._base_url}/action-result-string"
		params = {
			"text": text
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	def get_async_action_result_string(self, text: str | None) -> str | None:
		request_url = f"{self._base_url}/async-action-result-string"
		params = {
			"text": text
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return response.text
	
	def get_int(self, n: int | None) -> int | None:
		request_url = f"{self._base_url}/int"
		params = {
			"n": n
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	def get_async_int(self, n: int | None) -> int | None:
		request_url = f"{self._base_url}/async-int"
		params = {
			"n": n
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	def get_action_result_int(self, n: int | None) -> int | None:
		request_url = f"{self._base_url}/action-result-int"
		params = {
			"n": n
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	def get_async_action_result_int(self, n: int | None) -> int | None:
		request_url = f"{self._base_url}/async-action-result-int"
		params = {
			"n": n
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(int | None).validate_python(response.json())
	
	def get_date_time(self, v: datetime | None) -> datetime | None:
		request_url = f"{self._base_url}/datetime"
		params = {
			"v": v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat()
			if v is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	def get_async_date_time(self, v: datetime | None) -> datetime | None:
		request_url = f"{self._base_url}/async-datetime"
		params = {
			"v": v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat()
			if v is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	def get_action_result_date_time(self, v: datetime | None) -> datetime | None:
		request_url = f"{self._base_url}/action-result-datetime"
		params = {
			"v": v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat()
			if v is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	def get_async_action_result_date_time(self, v: datetime | None) -> datetime | None:
		request_url = f"{self._base_url}/async-action-result-datetime"
		params = {
			"v": v.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if v.tzinfo
			else v.isoformat()
			if v is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(datetime | None).validate_python(response.json())
	
	def get_my_dto(self, value: MyDto | None) -> MyDto | None:
		request_url = f"{self._base_url}/object"
		response = self._client.post(request_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	def get_async_my_dto(self, value: MyDto | None) -> MyDto | None:
		request_url = f"{self._base_url}/async-object"
		response = self._client.post(request_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	def action_result_object(self, value: MyDto | None) -> MyDto | None:
		request_url = f"{self._base_url}/action-result-object"
		response = self._client.post(request_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	def async_action_result_object(self, value: MyDto | None) -> MyDto | None:
		request_url = f"{self._base_url}/async-action-result-object"
		response = self._client.post(request_url, json = TypeAdapter(MyDto | None).dump_python(value, mode = "json"))
		response.raise_for_status()
		if (response.status_code == 204):
			return
		
		return TypeAdapter(MyDto | None).validate_python(response.json())
	
	def get_my_dto_nullable_array(self, values: list[MyDto | None]) -> list[MyDto | None]:
		request_url = f"{self._base_url}/nullable-array-return-type"
		response = self._client.post(request_url, json = TypeAdapter(list[MyDto | None]).dump_python(values, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(list[MyDto | None]).validate_python(response.json())
	
	def get_my_dto_collection(self, values: list[MyDto | None]) -> list[MyDto | None]:
		request_url = f"{self._base_url}/nullable-collection-return-type"
		response = self._client.post(request_url, json = TypeAdapter(list[MyDto | None]).dump_python(values, mode = "json"))
		response.raise_for_status()
		return TypeAdapter(list[MyDto | None]).validate_python(response.json())
	

