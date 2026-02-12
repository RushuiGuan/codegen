# @generated

from datetime import timezone, datetime, date
from dto import MyEnum
from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class FromQueryParamTestClient:
	_client: Session
	_base_url: str
	def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
		self._base_url = f"{base_url.rstrip('/')}/api/from-query-param-test"
		self._client = Session()
		self._client.auth = auth
	
	def close(self) -> None:
		self._client.close()
	
	def __enter__(self) -> Self:
		return self
	
	def __exit__(self, exc_type, exc_value, traceback) -> None:
		self.close()
	
	def required_string(self, name: str) -> None:
		request_url = f"{self._base_url}/required-string"
		params = {
			"name": name
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_string_implied(self, name: str) -> None:
		request_url = f"{self._base_url}/required-string-implied"
		params = {
			"name": name
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_string_diff_name(self, name: str) -> None:
		request_url = f"{self._base_url}/required-string-diff-name"
		params = {
			"n": name
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_int(self, value: int) -> None:
		request_url = f"{self._base_url}/required-int"
		params = {
			"value": value
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_date_time(self, datetime: datetime) -> None:
		request_url = f"{self._base_url}/required-datetime"
		params = {
			"datetime": datetime.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if datetime.tzinfo
			else datetime.isoformat()
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_date_time_diff_name(self, datetime: datetime) -> None:
		request_url = f"{self._base_url}/required-datetime_diff-name"
		params = {
			"d": datetime.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if datetime.tzinfo
			else datetime.isoformat()
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_date_only(self, dateonly: date) -> None:
		request_url = f"{self._base_url}/required-dateonly"
		params = {
			"dateonly": dateonly.isoformat()
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_date_only_diff_name(self, dateonly: date) -> None:
		request_url = f"{self._base_url}/required-dateonly_diff-name"
		params = {
			"d": dateonly.isoformat()
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_date_time_offset(self, date_time_offset: datetime) -> None:
		request_url = f"{self._base_url}/required-datetimeoffset"
		params = {
			"dateTimeOffset": date_time_offset.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if date_time_offset.tzinfo
			else date_time_offset.isoformat()
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_date_time_offset_diff_name(self, date_time_offset: datetime) -> None:
		request_url = f"{self._base_url}/required-datetimeoffset_diff-name"
		params = {
			"d": date_time_offset.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if date_time_offset.tzinfo
			else date_time_offset.isoformat()
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def required_enum_parameter(self, value: MyEnum) -> MyEnum:
		request_url = f"{self._base_url}/required-enum-parameter"
		params = {
			"value": value.value
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return TypeAdapter(MyEnum).validate_python(response.json())
	
	def nullable_string(self, name: str | None) -> None:
		request_url = f"{self._base_url}/nullable-string"
		params = {
			"name": name
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_string_implied(self, name: str | None) -> None:
		request_url = f"{self._base_url}/nullable-string-implied"
		params = {
			"name": name
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_string_diff_name(self, name: str | None) -> None:
		request_url = f"{self._base_url}/nullable-string-diff-name"
		params = {
			"n": name
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_int(self, value: int | None) -> None:
		request_url = f"{self._base_url}/nullable-int"
		params = {
			"value": value
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_date_time(self, datetime: datetime | None) -> None:
		request_url = f"{self._base_url}/nullable-datetime"
		params = {
			"datetime": datetime.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if datetime.tzinfo
			else datetime.isoformat()
			if datetime is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_date_time_diff_name(self, datetime: datetime | None) -> None:
		request_url = f"{self._base_url}/nullable-datetime_diff-name"
		params = {
			"d": datetime.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if datetime.tzinfo
			else datetime.isoformat()
			if datetime is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_date_only(self, dateonly: date | None) -> None:
		request_url = f"{self._base_url}/nullable-dateonly"
		params = {
			"dateonly": dateonly.isoformat()
			if dateonly is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_date_only_diff_name(self, dateonly: date | None) -> None:
		request_url = f"{self._base_url}/nullable-dateonly_diff-name"
		params = {
			"d": dateonly.isoformat()
			if dateonly is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_date_time_offset(self, date_time_offset: datetime | None) -> None:
		request_url = f"{self._base_url}/nullable-datetimeoffset"
		params = {
			"dateTimeOffset": date_time_offset.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if date_time_offset.tzinfo
			else date_time_offset.isoformat()
			if date_time_offset is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_date_time_offset_diff_name(self, date_time_offset: datetime | None) -> None:
		request_url = f"{self._base_url}/nullable-datetimeoffset_diff-name"
		params = {
			"d": date_time_offset.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
			if date_time_offset.tzinfo
			else date_time_offset.isoformat()
			if date_time_offset is not None
			else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
	
	def nullable_enum_parameter(self, value: MyEnum | None) -> MyEnum:
		request_url = f"{self._base_url}/nullable-enum-parameter"
		params = {
			"value": value.value if value is not None else None
		}
		response = self._client.get(request_url, params = params)
		response.raise_for_status()
		return TypeAdapter(MyEnum).validate_python(response.json())
	

