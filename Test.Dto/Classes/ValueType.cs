using System.Text.Json;
using Test.Dto.Enums;

namespace Test.Dto.Classes {
	public record class ValueType {
		public int IntValue { get; set; }
		public long LongValue { get; set; }
		public double DoubleValue { get; set; }
		public decimal DecimalValue { get; set; }
		public float FloatValue { get; set; }
		public bool BoolValue { get; set; }
		public char CharValue { get; set; }
		public short ShortValue { get; set; }
		public ushort UShortValue { get; set; }
		public uint UIntValue { get; set; }
		public ulong ULongValue { get; set; }
		public sbyte SByteValue { get; set; }
		public byte ByteValue { get; set; }
		public DateTime DateTimeValue { get; set; }
		public DateOnly DateOnlyValue { get; set; }
		public DateTimeOffset DateTimeOffsetValue { get; set; }
		public TimeSpan TimeSpanValue { get; set; }
		public TimeOnly TimeOnlyValue { get; set; }
		public Guid GuidValue { get; set; }
		public MyEnum EnumValue { get; set; }
		public MyStringEnum StringEnumValue { get; set; }
		public JsonElement JsonElementValue { get; set; }

		public int? NullableIntValue { get; set; }
		public long? NullableLongValue { get; set; }
		public double? NullableDoubleValue { get; set; }
		public decimal? NullableDecimalValue { get; set; }
		public float? NullableFloatValue { get; set; }
		public bool? NullableBoolValue { get; set; }
		public char? NullableCharValue { get; set; }
		public short? NullableShortValue { get; set; }
		public ushort? NullableUShortValue { get; set; }
		public uint? NullableUIntValue { get; set; }
		public ulong? NullableULongValue { get; set; }
		public sbyte? NullableSByteValue { get; set; }
		public byte? NullableByteValue { get; set; }
		public DateTime? NullableDateTimeValue { get; set; }
		public DateOnly? NullableDateOnlyValue { get; set; }
		public DateTimeOffset? NullableDateTimeOffsetValue { get; set; }
		public TimeSpan? NullableTimeSpanValue { get; set; }
		public TimeOnly? NullableTimeOnlyValue { get; set; }
		public Guid? NullableGuidValue { get; set; }
		public MyEnum? NullableEnumValue { get; set; }
		public MyStringEnum? NullableStringEnumValue { get; set; }
		public JsonElement? NullableJsonElementValue { get; set; }
	}
}