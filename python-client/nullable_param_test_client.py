# @generated

from datetime import date
from dto import MyDto, MyEnum
from pydantic import TypeAdapter
from requests import Session
from requests.auth import AuthBase
from typing import Self

class NullableParamTestClient:
    _client: Session
    _base_url: str
    def __init__(self, base_url: str, auth: AuthBase | None = None) -> None:
    	self._base_url = f"{base_url.rstrip('/')}/api/nullable-param-test"
    	self._client = Session()
    	self._client.auth = auth
    
    def close(self) -> None:
    	self._client.close()
    
    def __enter__(self) -> Self:
    	return self
    
    def __exit__(self, exc_type, exc_value, traceback) -> None:
    	self.close()
    
    def nullable_string_param(self, text: str | None) -> str:
    	request_url = f"{self._base_url}/nullable-string-param"
    	params = {
    	    "text": text
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_value_type(self, id: int | None) -> str:
    	request_url = f"{self._base_url}/nullable-value-type"
    	params = {
    	    "id": id
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_date_only(self, date: date | None) -> str:
    	request_url = f"{self._base_url}/nullable-date-only"
    	params = {
    	    "date": date.isoformat()
    	    if date is not None
    	    else None
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_post_param(self, dto: MyDto | None) -> None:
    	request_url = f"{self._base_url}/nullable-post-param"
    	response = self._client.post(request_url, json = TypeAdapter(MyDto | None).dump_python(dto, mode = "json"))
    	response.raise_for_status()
    
    def nullable_string_array(self, values: list[str | None]) -> str:
    	request_url = f"{self._base_url}/nullable-string-array"
    	params = {
    	    "values": [
    	        x if x is not None else ""
    	        for x in values
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_string_collection(self, values: list[str | None]) -> str:
    	request_url = f"{self._base_url}/nullable-string-collection"
    	params = {
    	    "values": [
    	        x if x is not None else ""
    	        for x in values
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_value_type_array(self, values: list[int | None]) -> str:
    	request_url = f"{self._base_url}/nullable-value-type-array"
    	params = {
    	    "values": [
    	        x if x is not None else ""
    	        for x in values
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_value_type_collection(self, values: list[int | None]) -> str:
    	request_url = f"{self._base_url}/nullable-value-type-collection"
    	params = {
    	    "values": [
    	        x if x is not None else ""
    	        for x in values
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_date_only_collection(self, dates: list[date | None]) -> str:
    	request_url = f"{self._base_url}/nullable-date-only-collection"
    	params = {
    	    "dates": [
    	        x.isoformat()
    	        if x is not None
    	        else ""
    	        for x in dates
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_date_only_array(self, dates: list[date | None]) -> str:
    	request_url = f"{self._base_url}/nullable-date-only-array"
    	params = {
    	    "dates": [
    	        x.isoformat()
    	        if x is not None
    	        else ""
    	        for x in dates
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return response.text
    
    def nullable_enum_parameter(self, value: MyEnum | None) -> MyEnum | None:
    	request_url = f"{self._base_url}/nullable-enum-parameter"
    	params = {
    	    "value": value.value if value is not None else None
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	if (response.status_code == 204):
    		return
    	
    	return TypeAdapter(MyEnum | None).validate_python(response.json())
    
    def nullable_enum_array(self, value: list[MyEnum | None]) -> list[MyEnum | None]:
    	request_url = f"{self._base_url}/nullable-enum-array"
    	params = {
    	    "value": [
    	        x.value if x is not None else ""
    	        for x in value
    	    ]
    	}
    	response = self._client.get(request_url, params = params)
    	response.raise_for_status()
    	return TypeAdapter(list[MyEnum | None]).validate_python(response.json())
    

