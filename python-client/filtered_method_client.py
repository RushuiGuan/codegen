# @generated

from requests import Session
from requests.auth import AuthBase
from typing import Self

class FilteredMethodClient:
    _client: Session
    _base_url: str
    def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
    	self._base_url = f"{base_url.rstrip('/')}/api/filtered-method"
    	self._client = Session()
    	self._client.auth = auth
    
    def close(self) -> None:
    	self._client.close()
    
    def __enter__(self) -> Self:
    	return self
    
    def __exit__(self, exc_type, exc_value, traceback) -> None:
    	self.close()
    
    def filtered_by_none(self) -> None:
    	request_url = f"{self._base_url}/none"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    

