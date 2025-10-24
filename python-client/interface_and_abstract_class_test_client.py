from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class InterfaceAndAbstractClassTestClient:
	base_url: str
	_client: AsyncClient
	def __init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
		self._client = AsyncClient(base_url = self.base_url, auth = HttpNtlmAuth(None, None))
	
	async def close(self):
		await self._client.aclose()
	
	async def __aenter__(self):
		return self
	
	async def __aexit__(self):
		await self.close()
	
	async def submit_by_interface(command: ICommand):
		relativeUrl = f"interface-as-param"
		result = this.doPostAsync[None, str](relativeUrl, "", { "command": command })
		return await xx(result)
	
	async def submit_by_abstract_class(command: AbstractClass):
		relativeUrl = f"abstract-class-as-param"
		result = this.doPostAsync[None, str](relativeUrl, "", { "command": command })
		return await xx(result)
	
	async def return_interface_async() -> ICommand:
		relativeUrl = f"return-interface-async"
		result = this.doPostAsync[ICommand, str](relativeUrl, "", {})
		return await xx(result)
	
	async def return_interface() -> ICommand:
		relativeUrl = f"return-interface"
		result = this.doPostAsync[ICommand, str](relativeUrl, "", {})
		return await xx(result)
	
	async def return_abstract_class_async() -> AbstractClass:
		relativeUrl = f"return-abstract-class-async"
		result = this.doPostAsync[AbstractClass, str](relativeUrl, "", {})
		return await xx(result)
	
	async def return_abstract_class() -> AbstractClass:
		relativeUrl = f"return-abstract-class"
		result = this.doPostAsync[AbstractClass, str](relativeUrl, "", {})
		return await xx(result)
	

