# @generated

from datetime import timezone, datetime, date, time
from httpx import AsyncClient, Auth
from typing import Self

class FromRoutingParamTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/from-routing-param-test"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def implicit_route(self, name: str, id: int) -> None:
		relative_url = f"implicit-route/{name}/{id}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def explicit_route(self, name: str, id: int) -> None:
		relative_url = f"explicit-route/{name}/{id}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def wild_card_route_double(self, name: str, id: int) -> None:
		relative_url = f"wild-card-route-double/{id}/{name}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def wild_card_route_single(self, name: str, id: int) -> None:
		relative_url = f"wild-card-route-single/{id}/{name}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def date_time_route(self, date: datetime, id: int) -> None:
		relative_url = f"date-time-route/{date.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
		if date.tzinfo
		else date.isoformat()}/{id}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def date_only_route(self, date: date, id: int) -> None:
		relative_url = f"date-only-route/{date.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
		if date.tzinfo
		else date.isoformat()}/{id}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def date_time_offset_route(self, date: datetime, id: int) -> None:
		relative_url = f"datetimeoffset-route/{date.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
		if date.tzinfo
		else date.isoformat()}/{id}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	
	async def time_only_route(self, time: time, id: int) -> None:
		relative_url = f"timeonly-route/{time.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
		if time.tzinfo
		else time.isoformat()}/{id}"
		response = await self._client.get(relative_url)
		response.raise_for_status()
	

