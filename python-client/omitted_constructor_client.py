# @generated

from requests import Session
from requests.auth import AuthBase
from typing import Self

class OmittedConstructorClient:
	_client: Session
	_base_url: str
	def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
		self._base_url = f"{base_url.rstrip('/')}/api/omittedconstructor"
		self._client = Session()
		self._client.auth = auth
	
	def close(self) -> None:
		self._client.close()
	
	def __enter__(self) -> Self:
		return self
	
	def __exit__(self, exc_type, exc_value, traceback) -> None:
		self.close()
	

