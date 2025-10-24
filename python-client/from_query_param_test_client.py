from datetime import datetime, date
from httpx import AsyncClient
from httpx_ntlm import HttpNtlmAuth

class FromQueryParamTestClient:
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
	
	async def required_string(name: str):
		relativeUrl = f"required-string"
		result = this.doGetAsync[None](relativeUrl, { "name": name })
		return await xx(result)
	
	async def required_string_implied(name: str):
		relativeUrl = f"required-string-implied"
		result = this.doGetAsync[None](relativeUrl, { "name": name })
		return await xx(result)
	
	async def required_string_diff_name(name: str):
		relativeUrl = f"required-string-diff-name"
		result = this.doGetAsync[None](relativeUrl, { "n": name })
		return await xx(result)
	
	async def required_date_time(datetime: datetime):
		relativeUrl = f"required-datetime"
		result = this.doGetAsync[None](relativeUrl, { "datetime": format(datetime, "yyyy-MM-ddTHH:mm:ssXXX") })
		return await xx(result)
	
	async def required_date_time_diff_name(datetime: datetime):
		relativeUrl = f"required-datetime_diff-name"
		result = this.doGetAsync[None](relativeUrl, { "d": format(datetime, "yyyy-MM-ddTHH:mm:ssXXX") })
		return await xx(result)
	
	async def required_date_only(dateonly: date):
		relativeUrl = f"required-dateonly"
		result = this.doGetAsync[None](relativeUrl, { "dateonly": format(dateonly, "yyyy-MM-dd") })
		return await xx(result)
	
	async def required_date_only_diff_name(dateonly: date):
		relativeUrl = f"required-dateonly_diff-name"
		result = this.doGetAsync[None](relativeUrl, { "d": format(dateonly, "yyyy-MM-dd") })
		return await xx(result)
	
	async def required_date_time_offset(dateTimeOffset: datetime):
		relativeUrl = f"required-datetimeoffset"
		result = this.doGetAsync[None](relativeUrl, { "dateTimeOffset": format(dateTimeOffset, "yyyy-MM-ddTHH:mm:ssXXX") })
		return await xx(result)
	
	async def required_date_time_offset_diff_name(dateTimeOffset: datetime):
		relativeUrl = f"required-datetimeoffset_diff-name"
		result = this.doGetAsync[None](relativeUrl, { "d": format(dateTimeOffset, "yyyy-MM-ddTHH:mm:ssXXX") })
		return await xx(result)
	

