# @generated

from dto import ICommand, AbstractClass
from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class InterfaceAndAbstractClassTestClient:
	_client: Session
	_base_url: str
	def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
		self._base_url = f"{base_url.rstrip('/')}/api/interface-abstract-class-test"
		self._client = Session()
		self._client.auth = auth
	
	def close(self) -> None:
		self._client.close()
	
	def __enter__(self) -> Self:
		return self
	
	def __exit__(self, exc_type, exc_value, traceback) -> None:
		self.close()
	
	def submit_by_interface(self, command: ICommand) -> None:
		request_url = f"{self._base_url}/interface-as-param"
		params = {
			"command": command
		}
		response = self._client.post(request_url, params = params)
		response.raise_for_status()
	
	def submit_by_abstract_class(self, command: AbstractClass) -> None:
		request_url = f"{self._base_url}/abstract-class-as-param"
		params = {
			"command": command
		}
		response = self._client.post(request_url, params = params)
		response.raise_for_status()
	
	def return_interface_async(self) -> ICommand:
		request_url = f"{self._base_url}/return-interface-async"
		response = self._client.post(request_url)
		response.raise_for_status()
		return TypeAdapter(ICommand).validate_python(response.json())
	
	def return_interface(self) -> ICommand:
		request_url = f"{self._base_url}/return-interface"
		response = self._client.post(request_url)
		response.raise_for_status()
		return TypeAdapter(ICommand).validate_python(response.json())
	
	def return_abstract_class_async(self) -> AbstractClass:
		request_url = f"{self._base_url}/return-abstract-class-async"
		response = self._client.post(request_url)
		response.raise_for_status()
		return TypeAdapter(AbstractClass).validate_python(response.json())
	
	def return_abstract_class(self) -> AbstractClass:
		request_url = f"{self._base_url}/return-abstract-class"
		response = self._client.post(request_url)
		response.raise_for_status()
		return TypeAdapter(AbstractClass).validate_python(response.json())
	

