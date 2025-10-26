from datetime import datetime, date, time
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class FromRouteParamTestClient:
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
	
	async def implicit_route(self, name: str, id: int):
		relative_url = f"implicit-route/{name}/{id}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def explicit_route(self, name: str, id: int):
		relative_url = f"explicit-route/{name}/{id}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def wild_card_route_double(self, name: str, id: int):
		relative_url = f"wild-card-route-double/{id}/{name}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def wild_card_route_single(self, name: str, id: int):
		relative_url = f"wild-card-route-single/{id}/{name}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def date_time_route(self, date: datetime, id: int):
		relative_url = f"date-time-route/{date.isoFormat()}/{id}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def date_time_as_date_only_route(self, date: datetime, id: int):
		relative_url = f"date-time-as-date-only-route/{date.isoFormat()}/{id}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def date_only_route(self, date: date, id: int):
		relative_url = f"date-only-route/{date.isoFormat()}/{id}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def date_time_offset_route(self, date: datetime, id: int):
		relative_url = f"datetimeoffset-route/{date.isoFormat()}/{id}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def time_only_route(self, time: time, id: int):
		relative_url = f"timeonly-route/{time.isoFormat()}/{id}"
		params = {}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	

