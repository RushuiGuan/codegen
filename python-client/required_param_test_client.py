# @generated

from datetime import date, timezone, datetime
from dto import MyDto, MyEnum
from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class RequiredParamTestClient:
    _client: Session
    _base_url: str
    def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
    	self._base_url = f"{base_url.rstrip('/')}/api/required-param-test"
    	self._client = Session()
    	self._client.auth = auth
    
    def close(self) -> None:
    	self._client.close()
    
    def __enter__(self) -> Self:
    	return self
    
    def __exit__(self, exc_type, exc_value, traceback) -> None:
    	self.close()
    
    def explicit_string_param(self, text: str) -> str:
    	request_url = f"{self._base_url}/explicit-string-param"
    	params = {
    	    "text": text
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def implicit_string_param(self, text: str) -> str:
    	request_url = f"{self._base_url}/implicit-string-param"
    	params = {
    	    "text": text
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_string_param(self, text: str) -> str:
    	request_url = f"{self._base_url}/required-string-param"
    	params = {
    	    "text": text
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_value_type(self, id: int) -> str:
    	request_url = f"{self._base_url}/required-value-type"
    	params = {
    	    "id": id
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_date_only(self, date: date) -> str:
    	request_url = f"{self._base_url}/required-date-only"
    	params = {
    	    "date": date.isoformat()
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_date_time(self, date: datetime) -> str:
    	request_url = f"{self._base_url}/required-datetime"
    	params = {
    	    "date": date.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
    	    if date.tzinfo
    	    else date.isoformat()
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_post_param(self, dto: MyDto) -> None:
    	request_url = f"{self._base_url}/required-post-param"
    	response = self._client.post(request_url, json = TypeAdapter(MyDto).dump_python(dto, mode = "json"))
    	response.raise_for_status()
    
    def required_string_array(self, values: list[str]) -> str:
    	request_url = f"{self._base_url}/required-string-array"
    	params = {
    	    "values": values
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_string_collection(self, values: list[str]) -> str:
    	request_url = f"{self._base_url}/required-string-collection"
    	params = {
    	    "values": values
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_value_type_array(self, values: list[int]) -> str:
    	request_url = f"{self._base_url}/required-value-type-array"
    	params = {
    	    "values": values
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_value_type_collection(self, values: list[int]) -> str:
    	request_url = f"{self._base_url}/required-value-type-collection"
    	params = {
    	    "values": values
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_date_only_collection(self, dates: list[date]) -> str:
    	request_url = f"{self._base_url}/required-date-only-collection"
    	params = {
    	    "dates": [
    	        x.isoformat()
    	        for x in dates
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_date_only_array(self, dates: list[date]) -> str:
    	request_url = f"{self._base_url}/required-date-only-array"
    	params = {
    	    "dates": [
    	        x.isoformat()
    	        for x in dates
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_date_time_collection(self, dates: list[datetime]) -> str:
    	request_url = f"{self._base_url}/required-datetime-collection"
    	params = {
    	    "dates": [
    	        x.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
    	        if x.tzinfo
    	        else x.isoformat()
    	        for x in dates
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_date_time_array(self, dates: list[datetime]) -> str:
    	request_url = f"{self._base_url}/required-datetime-array"
    	params = {
    	    "dates": [
    	        x.astimezone(timezone.utc).isoformat().replace("+00:00", "Z")
    	        if x.tzinfo
    	        else x.isoformat()
    	        for x in dates
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def required_enum(self, value: MyEnum) -> MyEnum:
    	request_url = f"{self._base_url}/required-enum"
    	params = {
    	    "value": value.value
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return TypeAdapter(MyEnum).validate_python(response.json())
    
    def required_enum_array(self, values: list[MyEnum]) -> list[MyEnum]:
    	request_url = f"{self._base_url}/required-enum-array"
    	params = {
    	    "values": [
    	        x.value
    	        for x in values
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return TypeAdapter(list[MyEnum]).validate_python(response.json())
    

