# @generated

from datetime import date, timezone, datetime
from requests import Session
from requests.auth import AuthBase
from typing import Self

class ArrayParamTestClient:
	_client: Session
	_base_url: str
	def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
		self._base_url = f"{base_url.rstrip('/')}/api/array-param-test"
		self._client = Session()
		self._client.auth = auth
	
	def close(self) -> None:
		self._client.close()
	
	def __enter__(self) -> Self:
		return self
	
	def __exit__(self, exc_type, exc_value, traceback) -> None:
		self.close()
	
	def array_string_param(self, array: list[str]) -> str:
		request_url = f"{self._base_url}/array-string-param"
		params = {
			"a": array
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return response.text
	
	def array_value_type(self, array: list[int]) -> str:
		request_url = f"{self._base_url}/array-value-type"
		params = {
			"a": array
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return response.text
	
	def collection_string_param(self, collection: list[str]) -> str:
		request_url = f"{self._base_url}/collection-string-param"
		params = {
			"c": collection
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return response.text
	
	def collection_value_type(self, collection: list[int]) -> str:
		request_url = f"{self._base_url}/collection-value-type"
		params = {
			"c": collection
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return response.text
	
	def collection_date_param(self, collection: list[date]) -> str:
		request_url = f"{self._base_url}/collection-date-param"
		params = {
			"c": [
				x.isoformat()
				for x in collection
			]
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return response.text
	
	def collection_date_time_param(self, collection: list[datetime]) -> str:
		request_url = f"{self._base_url}/collection-datetime-param"
		params = {
			"c": [
				x.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
				if x.tzinfo
				else x.isoformat()
				for x in collection
			]
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return response.text
	

