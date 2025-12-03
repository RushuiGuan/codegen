# @generated

from requests import Session
from requests.auth import AuthBase
from typing import Self

class DuplicateNameTestClient:
	_client: Session
	_base_url: str
	def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
		self._base_url = f"{base_url.rstrip('/')}/api/duplicate-name-test"
		self._client = Session()
		self._client.auth = auth
	
	def close(self) -> None:
		self._client.close()
	
	def __enter__(self) -> Self:
		return self
	
	def __exit__(self, exc_type, exc_value, traceback) -> None:
		self.close()
	
	def submit(self, id: int) -> None:
		request_url = f"{self._base_url}/by-id"
		params = {
			"id": id
		}
		response = self._client.post(request_url, params = params)
		response.raise_for_status()
	
	def submit1(self, name: str) -> None:
		request_url = f"{self._base_url}/by-name"
		params = {
			"name": name
		}
		response = self._client.post(request_url, params = params)
		response.raise_for_status()
	

