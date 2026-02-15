# @generated

from datetime import timezone, datetime, date, time
from dto import MyEnum
from requests import Session
from requests.auth import AuthBase
from typing import Self

class FromRoutingParamTestClient:
    _client: Session
    _base_url: str
    def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
    	self._base_url = f"{base_url.rstrip('/')}/api/from-routing-param-test"
    	self._client = Session()
    	self._client.auth = auth
    
    def close(self) -> None:
    	self._client.close()
    
    def __enter__(self) -> Self:
    	return self
    
    def __exit__(self, exc_type, exc_value, traceback) -> None:
    	self.close()
    
    def implicit_route(self, name: str, id: int) -> None:
    	request_url = f"{self._base_url}/implicit-route/{name}/{id}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def explicit_route(self, name: str, id: int) -> None:
    	request_url = f"{self._base_url}/explicit-route/{name}/{id}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def wild_card_route_double(self, name: str, id: int) -> None:
    	request_url = f"{self._base_url}/wild-card-route-double/{id}/{name}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def wild_card_route_single(self, name: str, id: int) -> None:
    	request_url = f"{self._base_url}/wild-card-route-single/{id}/{name}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def date_time_route(self, date: datetime, id: int) -> None:
    	request_url = f"{self._base_url}/date-time-route/{date.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
    	if date.tzinfo
    	else date.isoformat()}/{id}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def date_only_route(self, date: date, id: int) -> None:
    	request_url = f"{self._base_url}/date-only-route/{date.isoformat()}/{id}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def date_time_offset_route(self, date: datetime, id: int) -> None:
    	request_url = f"{self._base_url}/datetimeoffset-route/{date.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
    	if date.tzinfo
    	else date.isoformat()}/{id}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def time_only_route(self, time: time, id: int) -> None:
    	request_url = f"{self._base_url}/timeonly-route/{time.isoformat()}/{id}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    
    def enum_route(self, value: MyEnum, id: int) -> None:
    	request_url = f"{self._base_url}/enum-route/{value.value}/{id}"
    	response = self._client.get(request_url)
    	response.raise_for_status()
    

