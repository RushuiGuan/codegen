# @generated

from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class HttpMethodTestClient:
    _client: Session
    _base_url: str
    def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
    	self._base_url = f"{base_url.rstrip('/')}/api/http-method-test"
    	self._client = Session()
    	self._client.auth = auth
    
    def close(self) -> None:
    	self._client.close()
    
    def __enter__(self) -> Self:
    	return self
    
    def __exit__(self, exc_type, exc_value, traceback) -> None:
    	self.close()
    
    def delete(self) -> None:
    	request_url = f"{self._base_url}/"
    	response = self._client.delete(request_url)
    	response.raise_for_status()
    
    def post(self) -> None:
    	request_url = f"{self._base_url}/"
    	response = self._client.post(request_url)
    	response.raise_for_status()
    
    def patch(self) -> None:
    	request_url = f"{self._base_url}/"
    	response = self._client.patch(request_url)
    	response.raise_for_status()
    
    def get(self) -> int:
    	request_url = f"{self._base_url}/"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    	return TypeAdapter(int).validate_python(response.json())
    
    def put(self) -> None:
    	request_url = f"{self._base_url}/"
    	response = self._client.put(request_url)
    	response.raise_for_status()
    

