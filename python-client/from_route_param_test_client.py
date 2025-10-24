from datetime import datetime, date, time
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class FromRouteParamTestClient:
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
	
	async def implicit_route(name: str, id: int):
		relativeUrl = f"implicit-route/{name}/{id}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def explicit_route(name: str, id: int):
		relativeUrl = f"explicit-route/{name}/{id}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def wild_card_route_double(name: str, id: int):
		relativeUrl = f"wild-card-route-double/{id}/{name}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def wild_card_route_single(name: str, id: int):
		relativeUrl = f"wild-card-route-single/{id}/{name}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def date_time_route(date: datetime, id: int):
		relativeUrl = f"date-time-route/{format(date, "yyyy-MM-ddTHH:mm:ssXXX")}/{id}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def date_time_as_date_only_route(date: datetime, id: int):
		relativeUrl = f"date-time-as-date-only-route/{format(date, "yyyy-MM-ddTHH:mm:ssXXX")}/{id}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def date_only_route(date: date, id: int):
		relativeUrl = f"date-only-route/{format(date, "yyyy-MM-dd")}/{id}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def date_time_offset_route(date: datetime, id: int):
		relativeUrl = f"datetimeoffset-route/{format(date, "yyyy-MM-ddTHH:mm:ssXXX")}/{id}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	
	async def time_only_route(time: time, id: int):
		relativeUrl = f"timeonly-route/{format(time, "HH:mm:ss.SSS")}/{id}"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result)
	

