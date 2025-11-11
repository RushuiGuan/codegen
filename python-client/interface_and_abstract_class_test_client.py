# @generated

from dto import ICommand, AbstractClass
from httpx import AsyncClient, Auth
from pydantic import TypeAdapter
from typing import Self

class InterfaceAndAbstractClassTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/interface-abstract-class-test"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def submit_by_interface(self, command: ICommand) -> None:
		relative_url = "interface-as-param"
		params = {
			"command": command
		}
		response = await self._client.post(relative_url, params = params)
		response.raise_for_status()
	
	async def submit_by_abstract_class(self, command: AbstractClass) -> None:
		relative_url = "abstract-class-as-param"
		params = {
			"command": command
		}
		response = await self._client.post(relative_url, params = params)
		response.raise_for_status()
	
	async def return_interface_async(self) -> ICommand:
		relative_url = "return-interface-async"
		response = await self._client.post(relative_url)
		response.raise_for_status()
		return TypeAdapter(ICommand).validate_python(response.json())
	
	async def return_interface(self) -> ICommand:
		relative_url = "return-interface"
		response = await self._client.post(relative_url)
		response.raise_for_status()
		return TypeAdapter(ICommand).validate_python(response.json())
	
	async def return_abstract_class_async(self) -> AbstractClass:
		relative_url = "return-abstract-class-async"
		response = await self._client.post(relative_url)
		response.raise_for_status()
		return TypeAdapter(AbstractClass).validate_python(response.json())
	
	async def return_abstract_class(self) -> AbstractClass:
		relative_url = "return-abstract-class"
		response = await self._client.post(relative_url)
		response.raise_for_status()
		return TypeAdapter(AbstractClass).validate_python(response.json())
	

