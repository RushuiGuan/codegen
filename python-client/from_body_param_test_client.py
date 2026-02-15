# @generated

from dto import MyDto
from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class FromBodyParamTestClient:
    _client: Session
    _base_url: str
    def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
    	self._base_url = f"{base_url.rstrip('/')}/api/from-body-param-test"
    	self._client = Session()
    	self._client.auth = auth
    
    def close(self) -> None:
    	self._client.close()
    
    def __enter__(self) -> Self:
    	return self
    
    def __exit__(self, exc_type, exc_value, traceback) -> None:
    	self.close()
    
    def required_object(self, dto: MyDto) -> int:
    	request_url = f"{self._base_url}/required-object"
    	response = self._client.post(request_url, json = TypeAdapter(MyDto).dump_python(dto, mode = "json"))
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def nullable_object(self, dto: MyDto | None) -> int:
    	request_url = f"{self._base_url}/nullable-object"
    	response = self._client.post(request_url, json = TypeAdapter(MyDto | None).dump_python(dto, mode = "json"))
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def required_int(self, value: int) -> int:
    	request_url = f"{self._base_url}/required-int"
    	response = self._client.post(request_url, json = TypeAdapter(int).dump_python(value, mode = "json"))
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def nullable_int(self, value: int | None) -> int:
    	request_url = f"{self._base_url}/nullable-int"
    	response = self._client.post(request_url, json = TypeAdapter(int | None).dump_python(value, mode = "json"))
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def required_string(self, value: str) -> int:
    	request_url = f"{self._base_url}/required-string"
    	response = self._client.post(request_url, headers = {"Content-Type": "text/plain"}, data = value)
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def nullable_string(self, value: str | None) -> int:
    	request_url = f"{self._base_url}/nullable-string"
    	response = self._client.post(request_url, headers = {"Content-Type": "text/plain"}, data = value if value is not None else "")
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def required_object_array(self, array: list[MyDto]) -> int:
    	request_url = f"{self._base_url}/required-object-array"
    	response = self._client.post(request_url, json = TypeAdapter(list[MyDto]).dump_python(array, mode = "json"))
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def nullable_object_array(self, array: list[MyDto | None]) -> int:
    	request_url = f"{self._base_url}/nullable-object-array"
    	response = self._client.post(request_url, json = TypeAdapter(list[MyDto | None]).dump_python(array, mode = "json"))
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    

