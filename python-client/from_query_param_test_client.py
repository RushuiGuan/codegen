from datetime import datetime, date
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class FromQueryParamTestClient:
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
	
	async def required_string(self, name: str):
		relative_url = f"required-string"
		params = { "name": name }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_string_implied(self, name: str):
		relative_url = f"required-string-implied"
		params = { "name": name }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_string_diff_name(self, name: str):
		relative_url = f"required-string-diff-name"
		params = { "n": name }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time(self, datetime: datetime):
		relative_url = f"required-datetime"
		params = { "datetime": datetime.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_diff_name(self, datetime: datetime):
		relative_url = f"required-datetime_diff-name"
		params = { "d": datetime.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_only(self, dateonly: date):
		relative_url = f"required-dateonly"
		params = { "dateonly": dateonly.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_only_diff_name(self, dateonly: date):
		relative_url = f"required-dateonly_diff-name"
		params = { "d": dateonly.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_offset(self, date_time_offset: datetime):
		relative_url = f"required-datetimeoffset"
		params = { "dateTimeOffset": date_time_offset.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	
	async def required_date_time_offset_diff_name(self, date_time_offset: datetime):
		relative_url = f"required-datetimeoffset_diff-name"
		params = { "d": date_time_offset.isoFormat() }
		response = await self._client.get(relative_url, params = params)
		response.raise_for_status()
	

