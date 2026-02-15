# @generated

from __future__ import annotations
from datetime import datetime, date, timedelta, time
from decimal import Decimal
from enum import Enum
from pydantic import BaseModel, ConfigDict, Field
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
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    name: str = Field(alias = "name")
    value: int = Field(alias = "value")

class ArrayValueType(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    int_array: list[int] = Field(alias = "intArray", default_factory = list)
    nullable_int_array: list[int | None] = Field(alias = "nullableIntArray", default_factory = list)

class BaseType(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    id: int = Field(alias = "id")

class Command1(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    name: str = Field(alias = "name")

class Command2(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    name: str = Field(alias = "name")

class CollectionValueType(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    int_collection: list[int] = Field(alias = "intCollection", default_factory = list)
    nullable_int_collection: list[int | None] = Field(alias = "nullableIntCollection", default_factory = list)

class MyClassWithBaseType(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    id: int = Field(alias = "id")

class MyClassWithGenericBaseType(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    value: str = Field(alias = "value")
    name: str = Field(alias = "name")

class MyDto(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    name: str = Field(alias = "name")
    string_long_name: str = Field(alias = "stringLongName")
    byte_array: bytes = Field(alias = "byteArray", default_factory = list)
    int_value: int = Field(alias = "intValue")
    long_value: int = Field(alias = "longValue")
    double_value: float = Field(alias = "doubleValue")
    decimal_value: Decimal = Field(alias = "decimalValue")
    decimal_long_name: Decimal = Field(alias = "decimalLongName")
    float_value: float = Field(alias = "floatValue")
    bool_value: bool = Field(alias = "boolValue", default = False)
    char_value: str = Field(alias = "charValue")
    char_array: str = Field(alias = "charArray", default_factory = list)
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
    nullable_int_value: int | None = Field(alias = "nullableIntValue", default = None)
    nullable_int_generic: list[int | None] = Field(alias = "nullableIntGeneric", default_factory = list)
    nullable_string_value: str | None = Field(alias = "nullableStringValue", default = None)
    nullable_string_array: list[str | None] = Field(alias = "nullableStringArray", default_factory = list)
    guid_value: UUID = Field(alias = "guidValue")
    enum_value: MyEnum = Field(alias = "enumValue")
    string_enum_value: MyStringEnum = Field(alias = "stringEnumValue")
    int_array: list[int] = Field(alias = "intArray", default_factory = list)
    enumerable: list[Any] = Field(alias = "enumerable", default_factory = list)
    generic_int_enumerable: list[int] = Field(alias = "genericIntEnumerable", default_factory = list)

class ReferenceType(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    string_value: str = Field(alias = "stringValue")
    object_value: object = Field(alias = "objectValue")
    my_class: MyClassWithGenericBaseType = Field(alias = "myClass")
    nullable_string: str | None = Field(alias = "nullableString", default = None)
    nullable_my_class: MyClassWithGenericBaseType | None = Field(alias = "nullableMyClass", default = None)
    nullable_object: None | object = Field(alias = "nullableObject", default = None)

class ValueType(BaseModel):
    model_config = ConfigDict(populate_by_name = True, serialize_by_alias = True)
    int_value: int = Field(alias = "intValue")
    long_value: int = Field(alias = "longValue")
    double_value: float = Field(alias = "doubleValue")
    decimal_value: Decimal = Field(alias = "decimalValue")
    float_value: float = Field(alias = "floatValue")
    bool_value: bool = Field(alias = "boolValue", default = False)
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
    nullable_int_value: int | None = Field(alias = "nullableIntValue", default = None)
    nullable_long_value: int | None = Field(alias = "nullableLongValue", default = None)
    nullable_double_value: float | None = Field(alias = "nullableDoubleValue", default = None)
    nullable_decimal_value: Decimal | None = Field(alias = "nullableDecimalValue", default = None)
    nullable_float_value: float | None = Field(alias = "nullableFloatValue", default = None)
    nullable_bool_value: bool | None = Field(alias = "nullableBoolValue", default = None)
    nullable_char_value: str | None = Field(alias = "nullableCharValue", default = None)
    nullable_short_value: int | None = Field(alias = "nullableShortValue", default = None)
    nullable_u_short_value: int | None = Field(alias = "nullableUShortValue", default = None)
    nullable_u_int_value: int | None = Field(alias = "nullableUIntValue", default = None)
    nullable_u_long_value: int | None = Field(alias = "nullableULongValue", default = None)
    nullable_s_byte_value: int | None = Field(alias = "nullableSByteValue", default = None)
    nullable_byte_value: int | None = Field(alias = "nullableByteValue", default = None)
    nullable_date_time_value: datetime | None = Field(alias = "nullableDateTimeValue", default = None)
    nullable_date_only_value: date | None = Field(alias = "nullableDateOnlyValue", default = None)
    nullable_date_time_offset_value: datetime | None = Field(alias = "nullableDateTimeOffsetValue", default = None)
    nullable_time_span_value: timedelta | None = Field(alias = "nullableTimeSpanValue", default = None)
    nullable_time_only_value: time | None = Field(alias = "nullableTimeOnlyValue", default = None)
    nullable_guid_value: UUID | None = Field(alias = "nullableGuidValue", default = None)
    nullable_enum_value: MyEnum | None = Field(alias = "nullableEnumValue", default = None)
    nullable_string_enum_value: MyStringEnum | None = Field(alias = "nullableStringEnumValue", default = None)
    nullable_json_element_value: Any | None = Field(alias = "nullableJsonElementValue", default = None)

