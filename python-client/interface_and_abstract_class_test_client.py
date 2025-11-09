# @generated

from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class InterfaceAndAbstractClassTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str):
		base_url = base_url.rstrip("/")
		self._client = AsyncClient(base_url = base_url, auth = HttpNtlmAuth(None, None))
	
	async def close(self):
		await self._client.aclose()
	
	async def __aenter__(self):
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback):
		await self.close()
	
	async def submit_by_interface(self, command: ICommand):
		relative_url = "interface-as-param"
		params = { "command": command }
		response = await self._client.post[str](relative_url, "", params = params)
		response.raise_for_status()
	
	async def submit_by_abstract_class(self, command: AbstractClass):
		relative_url = "abstract-class-as-param"
		params = { "command": command }
		response = await self._client.post[str](relative_url, "", params = params)
		response.raise_for_status()
	
	async def return_interface_async(self) -> ICommand:
		relative_url = "return-interface-async"
		response = self._client.post[str](relative_url, "")
		response.raise_for_status()
	
	async def return_interface(self) -> ICommand:
		relative_url = "return-interface"
		response = self._client.post[str](relative_url, "")
		response.raise_for_status()
	
	async def return_abstract_class_async(self) -> AbstractClass:
		relative_url = "return-abstract-class-async"
		response = self._client.post[str](relative_url, "")
		response.raise_for_status()
	
	async def return_abstract_class(self) -> AbstractClass:
		relative_url = "return-abstract-class"
		response = self._client.post[str](relative_url, "")
		response.raise_for_status()
	

