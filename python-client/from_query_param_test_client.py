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
	
	async def __aexit__(self):
		await self.close()
	
	async def required_string(self, name: str):
		relativeUrl = f"required-string"
		response = await self._client.get(relativeUrl, { "name": name })
		response.raise_for_status()
	
	async def required_string_implied(self, name: str):
		relativeUrl = f"required-string-implied"
		response = await self._client.get(relativeUrl, { "name": name })
		response.raise_for_status()
	
	async def required_string_diff_name(self, name: str):
		relativeUrl = f"required-string-diff-name"
		response = await self._client.get(relativeUrl, { "n": name })
		response.raise_for_status()
	
	async def required_date_time(self, datetime: datetime):
		relativeUrl = f"required-datetime"
		response = await self._client.get(relativeUrl, { "datetime": format(datetime, "yyyy-MM-ddTHH:mm:ssXXX") })
		response.raise_for_status()
	
	async def required_date_time_diff_name(self, datetime: datetime):
		relativeUrl = f"required-datetime_diff-name"
		response = await self._client.get(relativeUrl, { "d": format(datetime, "yyyy-MM-ddTHH:mm:ssXXX") })
		response.raise_for_status()
	
	async def required_date_only(self, dateonly: date):
		relativeUrl = f"required-dateonly"
		response = await self._client.get(relativeUrl, { "dateonly": format(dateonly, "yyyy-MM-dd") })
		response.raise_for_status()
	
	async def required_date_only_diff_name(self, dateonly: date):
		relativeUrl = f"required-dateonly_diff-name"
		response = await self._client.get(relativeUrl, { "d": format(dateonly, "yyyy-MM-dd") })
		response.raise_for_status()
	
	async def required_date_time_offset(self, dateTimeOffset: datetime):
		relativeUrl = f"required-datetimeoffset"
		response = await self._client.get(relativeUrl, { "dateTimeOffset": format(dateTimeOffset, "yyyy-MM-ddTHH:mm:ssXXX") })
		response.raise_for_status()
	
	async def required_date_time_offset_diff_name(self, dateTimeOffset: datetime):
		relativeUrl = f"required-datetimeoffset_diff-name"
		response = await self._client.get(relativeUrl, { "d": format(dateTimeOffset, "yyyy-MM-ddTHH:mm:ssXXX") })
		response.raise_for_status()
	

