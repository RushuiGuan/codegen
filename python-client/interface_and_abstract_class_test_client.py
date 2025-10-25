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
	
	async def __aexit__(self):
		await self.close()
	
	async def submit_by_interface(self, command: ICommand):
		relativeUrl = f"interface-as-param"
		response = await self._client.post[str](relativeUrl, "", { "command": command })
		response.raise_for_status()
	
	async def submit_by_abstract_class(self, command: AbstractClass):
		relativeUrl = f"abstract-class-as-param"
		response = await self._client.post[str](relativeUrl, "", { "command": command })
		response.raise_for_status()
	
	async def return_interface_async(self) -> ICommand:
		relativeUrl = f"return-interface-async"
		response = await self._client.post[str](relativeUrl, "", {})
		response.raise_for_status()
	
	async def return_interface(self) -> ICommand:
		relativeUrl = f"return-interface"
		response = await self._client.post[str](relativeUrl, "", {})
		response.raise_for_status()
	
	async def return_abstract_class_async(self) -> AbstractClass:
		relativeUrl = f"return-abstract-class-async"
		response = await self._client.post[str](relativeUrl, "", {})
		response.raise_for_status()
	
	async def return_abstract_class(self) -> AbstractClass:
		relativeUrl = f"return-abstract-class"
		response = await self._client.post[str](relativeUrl, "", {})
		response.raise_for_status()
	

