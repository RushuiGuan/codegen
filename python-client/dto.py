from dataclasses import dataclass
from datetime import datetime, date, timedelta, time
from decimal import Decimal
from enum import Enum
from typing import Any
from uuid import UUID

class MyEnum(Enum):
	ONE = 0
	TWO = 1
	THREE = 2

class MyStringEnum(Enum):
	ONE = "One"
	TWO = "Two"
	THREE = "Three"

@dataclass()
class DerivedClass:
	name: str
	value: int

@dataclass()
class ArrayValueType:
	int_array: list[int]
	nullable_int_array: list[int|None]

@dataclass()
class BaseType:
	id: int

@dataclass()
class Command1:
	name: str

@dataclass()
class Command2:
	name: str

@dataclass()
class CollectionValueType:
	int_collection: list[int]
	nullable_int_collection: list[int|None]

@dataclass()
class MyClassWithBaseType:
	id: int

@dataclass()
class MyClassWithGenericBaseType:
	value: str
	name: str

@dataclass()
class MyDto:
	name: str
	string_long_name: str
	byte_array: bytes
	int_value: int
	long_value: int
	double_value: float
	decimal_value: Decimal
	decimal_long_name: Decimal
	float_value: float
	bool_value: bool
	char_value: str
	char_array: str
	short_value: int
	u_short_value: int
	u_int_value: int
	u_long_value: int
	s_byte_value: int
	byte_value: int
	date_value: datetime
	date_time_value: datetime
	date_only_value: date
	date_time_offset_value: datetime
	time_span_value: timedelta
	time_only_value: time
	nullable_int_value: int|None
	nullable_int_generic: list[int|None]
	nullable_string_value: None|str
	nullable_string_array: list[None|str]
	guid_value: UUID
	enum_value: MyEnum
	string_enum_value: MyStringEnum
	int_array: list[int]
	enumerable: list[Any]
	generic_int_enumerable: list[int]

@dataclass()
class ReferenceType:
	string_value: str
	object_value: object
	my_class: MyClassWithGenericBaseType
	nullable_string: None|str
	nullable_my_class: MyClassWithGenericBaseType|None
	nullable_object: None|object

@dataclass()
class ValueType:
	int_value: int
	long_value: int
	double_value: float
	decimal_value: Decimal
	float_value: float
	bool_value: bool
	char_value: str
	short_value: int
	u_short_value: int
	u_int_value: int
	u_long_value: int
	s_byte_value: int
	byte_value: int
	date_time_value: datetime
	date_only_value: date
	date_time_offset_value: datetime
	time_span_value: timedelta
	time_only_value: time
	guid_value: UUID
	enum_value: MyEnum
	string_enum_value: MyStringEnum
	json_element_value: Any
	nullable_int_value: int|None
	nullable_long_value: int|None
	nullable_double_value: float|None
	nullable_decimal_value: Decimal|None
	nullable_float_value: float|None
	nullable_bool_value: bool|None
	nullable_char_value: str|None
	nullable_short_value: int|None
	nullable_u_short_value: int|None
	nullable_u_int_value: int|None
	nullable_u_long_value: int|None
	nullable_s_byte_value: int|None
	nullable_byte_value: int|None
	nullable_date_time_value: datetime|None
	nullable_date_only_value: date|None
	nullable_date_time_offset_value: datetime|None
	nullable_time_span_value: timedelta|None
	nullable_time_only_value: time|None
	nullable_guid_value: UUID|None
	nullable_enum_value: MyEnum|None
	nullable_string_enum_value: MyStringEnum|None
	nullable_json_element_value: Any|None

