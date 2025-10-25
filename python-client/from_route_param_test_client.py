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
	
	async def __aexit__(self):
		await self.close()
	
	async def implicit_route(self, name: str, id: int):
		relativeUrl = f"implicit-route/{name}/{id}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def explicit_route(self, name: str, id: int):
		relativeUrl = f"explicit-route/{name}/{id}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def wild_card_route_double(self, name: str, id: int):
		relativeUrl = f"wild-card-route-double/{id}/{name}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def wild_card_route_single(self, name: str, id: int):
		relativeUrl = f"wild-card-route-single/{id}/{name}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def date_time_route(self, date: datetime, id: int):
		relativeUrl = f"date-time-route/{format(date, "yyyy-MM-ddTHH:mm:ssXXX")}/{id}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def date_time_as_date_only_route(self, date: datetime, id: int):
		relativeUrl = f"date-time-as-date-only-route/{format(date, "yyyy-MM-ddTHH:mm:ssXXX")}/{id}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def date_only_route(self, date: date, id: int):
		relativeUrl = f"date-only-route/{format(date, "yyyy-MM-dd")}/{id}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def date_time_offset_route(self, date: datetime, id: int):
		relativeUrl = f"datetimeoffset-route/{format(date, "yyyy-MM-ddTHH:mm:ssXXX")}/{id}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	
	async def time_only_route(self, time: time, id: int):
		relativeUrl = f"timeonly-route/{format(time, "HH:mm:ss.SSS")}/{id}"
		response = await self._client.get(relativeUrl, {})
		response.raise_for_status()
	

