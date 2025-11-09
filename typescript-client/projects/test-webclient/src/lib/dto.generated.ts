
export enum MyEnum {
	One = 0,
	Two = 1,
	Three = 2
}
export enum MyStringEnum {
	One = "One",
	Two = "Two",
	Three = "Three"
}
export interface DerivedClass {
	name: string;
	value: number;
}
export interface ArrayValueType {
	intArray: number[];
	nullableIntArray: (number|undefined)[];
}
export interface BaseType {
	id: number;
}
export interface Command1 {
	name: string;
}
export interface Command2 {
	name: string;
}
export interface CollectionValueType {
	intCollection: number[];
	nullableIntCollection: (number|undefined)[];
}
export interface MyClassWithBaseType {
	id: number;
}
export interface MyClassWithGenericBaseType {
	value: string;
	name: string;
}
export interface MyDto {
	name: string;
	stringLongName: string;
	byteArray: string;
	intValue: number;
	longValue: number;
	doubleValue: number;
	decimalValue: number;
	decimalLongName: number;
	floatValue: number;
	boolValue: boolean;
	charValue: string;
	charArray: string;
	shortValue: number;
	uShortValue: number;
	uIntValue: number;
	uLongValue: number;
	sByteValue: number;
	byteValue: number;
	dateValue: Date;
	dateTimeValue: Date;
	dateOnlyValue: Date;
	dateTimeOffsetValue: Date;
	timeSpanValue: Date;
	timeOnlyValue: Date;
	nullableIntValue ?: number|undefined;
	nullableIntGeneric: (number|undefined)[];
	nullableStringValue ?: string;
	nullableStringArray: string[];
	guidValue: string;
	enumValue: MyEnum;
	stringEnumValue: MyStringEnum;
	intArray: number[];
	enumerable: any[];
	genericIntEnumerable: number[];
}
export interface ReferenceType {
	stringValue: string;
	objectValue: object;
	myClass: MyClassWithGenericBaseType;
	nullableString ?: string;
	nullableMyClass ?: MyClassWithGenericBaseType|undefined;
	nullableObject ?: object;
}
export interface ValueType {
	intValue: number;
	longValue: number;
	doubleValue: number;
	decimalValue: number;
	floatValue: number;
	boolValue: boolean;
	charValue: string;
	shortValue: number;
	uShortValue: number;
	uIntValue: number;
	uLongValue: number;
	sByteValue: number;
	byteValue: number;
	dateTimeValue: Date;
	dateOnlyValue: Date;
	dateTimeOffsetValue: Date;
	timeSpanValue: Date;
	timeOnlyValue: Date;
	guidValue: string;
	enumValue: MyEnum;
	stringEnumValue: MyStringEnum;
	jsonElementValue: object;
	nullableIntValue ?: number|undefined;
	nullableLongValue ?: number|undefined;
	nullableDoubleValue ?: number|undefined;
	nullableDecimalValue ?: number|undefined;
	nullableFloatValue ?: number|undefined;
	nullableBoolValue ?: boolean|undefined;
	nullableCharValue ?: string|undefined;
	nullableShortValue ?: number|undefined;
	nullableUShortValue ?: number|undefined;
	nullableUIntValue ?: number|undefined;
	nullableULongValue ?: number|undefined;
	nullableSByteValue ?: number|undefined;
	nullableByteValue ?: number|undefined;
	nullableDateTimeValue ?: Date|undefined;
	nullableDateOnlyValue ?: Date|undefined;
	nullableDateTimeOffsetValue ?: Date|undefined;
	nullableTimeSpanValue ?: Date|undefined;
	nullableTimeOnlyValue ?: Date|undefined;
	nullableGuidValue ?: string|undefined;
	nullableEnumValue ?: MyEnum|undefined;
	nullableStringEnumValue ?: MyStringEnum|undefined;
	nullableJsonElementValue ?: object|undefined;
}
