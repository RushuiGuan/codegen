# @generated

from __future__ import annotations
from datetime import datetime, date, timedelta, time
from decimal import Decimal
from enum import Enum
from pydantic import BaseModel, Field
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

class DerivedClass(BaseModel):
	name: str = Field(alias = "name")
	value: int = Field(alias = "value")

class ArrayValueType(BaseModel):
	int_array: list[int] = Field(alias = "intArray")
	nullable_int_array: list[int | None] = Field(alias = "nullableIntArray")

class BaseType(BaseModel):
	id: int = Field(alias = "id")

class Command1(BaseModel):
	name: str = Field(alias = "name")

class Command2(BaseModel):
	name: str = Field(alias = "name")

class CollectionValueType(BaseModel):
	int_collection: list[int] = Field(alias = "intCollection")
	nullable_int_collection: list[int | None] = Field(alias = "nullableIntCollection")

class MyClassWithBaseType(BaseModel):
	id: int = Field(alias = "id")

class MyClassWithGenericBaseType(BaseModel):
	value: str = Field(alias = "value")
	name: str = Field(alias = "name")

class MyDto(BaseModel):
	name: str = Field(alias = "name")
	string_long_name: str = Field(alias = "stringLongName")
	byte_array: bytes = Field(alias = "byteArray")
	int_value: int = Field(alias = "intValue")
	long_value: int = Field(alias = "longValue")
	double_value: float = Field(alias = "doubleValue")
	decimal_value: Decimal = Field(alias = "decimalValue")
	decimal_long_name: Decimal = Field(alias = "decimalLongName")
	float_value: float = Field(alias = "floatValue")
	bool_value: bool = Field(alias = "boolValue")
	char_value: str = Field(alias = "charValue")
	char_array: str = Field(alias = "charArray")
	short_value: int = Field(alias = "shortValue")
	u_short_value: int = Field(alias = "uShortValue")
	u_int_value: int = Field(alias = "uIntValue")
	u_long_value: int = Field(alias = "uLongValue")
	s_byte_value: int = Field(alias = "sByteValue")
	byte_value: int = Field(alias = "byteValue")
	date_value: datetime = Field(alias = "dateValue")
	date_time_value: datetime = Field(alias = "dateTimeValue")
	date_only_value: date = Field(alias = "dateOnlyValue")
	date_time_offset_value: datetime = Field(alias = "dateTimeOffsetValue")
	time_span_value: timedelta = Field(alias = "timeSpanValue")
	time_only_value: time = Field(alias = "timeOnlyValue")
	nullable_int_value: int | None = Field(alias = "nullableIntValue")
	nullable_int_generic: list[int | None] = Field(alias = "nullableIntGeneric")
	nullable_string_value: str | None = Field(alias = "nullableStringValue")
	nullable_string_array: list[str | None] = Field(alias = "nullableStringArray")
	guid_value: UUID = Field(alias = "guidValue")
	enum_value: MyEnum = Field(alias = "enumValue")
	string_enum_value: MyStringEnum = Field(alias = "stringEnumValue")
	int_array: list[int] = Field(alias = "intArray")
	enumerable: list[Any] = Field(alias = "enumerable")
	generic_int_enumerable: list[int] = Field(alias = "genericIntEnumerable")

class ReferenceType(BaseModel):
	string_value: str = Field(alias = "stringValue")
	object_value: object = Field(alias = "objectValue")
	my_class: MyClassWithGenericBaseType = Field(alias = "myClass")
	nullable_string: str | None = Field(alias = "nullableString")
	nullable_my_class: MyClassWithGenericBaseType | None = Field(alias = "nullableMyClass")
	nullable_object: None | object = Field(alias = "nullableObject")

class ValueType(BaseModel):
	int_value: int = Field(alias = "intValue")
	long_value: int = Field(alias = "longValue")
	double_value: float = Field(alias = "doubleValue")
	decimal_value: Decimal = Field(alias = "decimalValue")
	float_value: float = Field(alias = "floatValue")
	bool_value: bool = Field(alias = "boolValue")
	char_value: str = Field(alias = "charValue")
	short_value: int = Field(alias = "shortValue")
	u_short_value: int = Field(alias = "uShortValue")
	u_int_value: int = Field(alias = "uIntValue")
	u_long_value: int = Field(alias = "uLongValue")
	s_byte_value: int = Field(alias = "sByteValue")
	byte_value: int = Field(alias = "byteValue")
	date_time_value: datetime = Field(alias = "dateTimeValue")
	date_only_value: date = Field(alias = "dateOnlyValue")
	date_time_offset_value: datetime = Field(alias = "dateTimeOffsetValue")
	time_span_value: timedelta = Field(alias = "timeSpanValue")
	time_only_value: time = Field(alias = "timeOnlyValue")
	guid_value: UUID = Field(alias = "guidValue")
	enum_value: MyEnum = Field(alias = "enumValue")
	string_enum_value: MyStringEnum = Field(alias = "stringEnumValue")
	json_element_value: Any = Field(alias = "jsonElementValue")
	nullable_int_value: int | None = Field(alias = "nullableIntValue")
	nullable_long_value: int | None = Field(alias = "nullableLongValue")
	nullable_double_value: float | None = Field(alias = "nullableDoubleValue")
	nullable_decimal_value: Decimal | None = Field(alias = "nullableDecimalValue")
	nullable_float_value: float | None = Field(alias = "nullableFloatValue")
	nullable_bool_value: bool | None = Field(alias = "nullableBoolValue")
	nullable_char_value: str | None = Field(alias = "nullableCharValue")
	nullable_short_value: int | None = Field(alias = "nullableShortValue")
	nullable_u_short_value: int | None = Field(alias = "nullableUShortValue")
	nullable_u_int_value: int | None = Field(alias = "nullableUIntValue")
	nullable_u_long_value: int | None = Field(alias = "nullableULongValue")
	nullable_s_byte_value: int | None = Field(alias = "nullableSByteValue")
	nullable_byte_value: int | None = Field(alias = "nullableByteValue")
	nullable_date_time_value: datetime | None = Field(alias = "nullableDateTimeValue")
	nullable_date_only_value: date | None = Field(alias = "nullableDateOnlyValue")
	nullable_date_time_offset_value: datetime | None = Field(alias = "nullableDateTimeOffsetValue")
	nullable_time_span_value: timedelta | None = Field(alias = "nullableTimeSpanValue")
	nullable_time_only_value: time | None = Field(alias = "nullableTimeOnlyValue")
	nullable_guid_value: UUID | None = Field(alias = "nullableGuidValue")
	nullable_enum_value: MyEnum | None = Field(alias = "nullableEnumValue")
	nullable_string_enum_value: MyStringEnum | None = Field(alias = "nullableStringEnumValue")
	nullable_json_element_value: Any | None = Field(alias = "nullableJsonElementValue")

