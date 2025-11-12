# @generated

from datetime import timezone, datetime, date
from dto import MyEnum
from httpx import AsyncClient, Auth
from pydantic import TypeAdapter
from typing import Self

class FromQueryParamTestClient:
	_client: AsyncClient
	def __init__(self, base_url: str, auth: Auth | None = None) -> None:
		base_url = f"{base_url.rstrip('/')}/api/from-query-param-test"
		self._client = AsyncClient(base_url = base_url, auth = auth)
	
	async def close(self) -> None:
		await self._client.aclose()
	
	async def __aenter__(self) -> Self:
		return self
	
	async def __aexit__(self, exc_type, exc_value, traceback) -> None:
		await self.close()
	
	async def required_string(self, name: str) -> None:
		relative_url = "required-string"
		params = {
			"name": name
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_string_implied(self, name: str) -> None:
		relative_url = "required-string-implied"
		params = {
			"name": name
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_string_diff_name(self, name: str) -> None:
		relative_url = "required-string-diff-name"
		params = {
			"n": name
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time(self, datetime: datetime) -> None:
		relative_url = "required-datetime"
		params = {
			"datetime": datetime.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if datetime.tzinfo
			else datetime.isoformat()
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_diff_name(self, datetime: datetime) -> None:
		relative_url = "required-datetime_diff-name"
		params = {
			"d": datetime.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if datetime.tzinfo
			else datetime.isoformat()
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_only(self, dateonly: date) -> None:
		relative_url = "required-dateonly"
		params = {
			"dateonly": dateonly.isoformat()
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_only_diff_name(self, dateonly: date) -> None:
		relative_url = "required-dateonly_diff-name"
		params = {
			"d": dateonly.isoformat()
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_offset(self, date_time_offset: datetime) -> None:
		relative_url = "required-datetimeoffset"
		params = {
			"dateTimeOffset": date_time_offset.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if date_time_offset.tzinfo
			else date_time_offset.isoformat()
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_offset_diff_name(self, date_time_offset: datetime) -> None:
		relative_url = "required-datetimeoffset_diff-name"
		params = {
			"d": date_time_offset.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if date_time_offset.tzinfo
			else date_time_offset.isoformat()
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_enum_parameter(self, value: MyEnum) -> MyEnum:
		relative_url = "required-enum-parameter"
		params = {
			"value": value.value
		}
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
		return TypeAdapter(MyEnum).validate_python(response.json())
	

