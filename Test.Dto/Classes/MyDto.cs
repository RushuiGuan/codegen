using System.Collections;
using System.Text.Json.Serialization;
using Test.Dto.Enums;

namespace Test.Dto.Classes {
	public class MyDto {
		public string Name { get; set; } = string.Empty;
		public string StringLongName { get; set; } = string.Empty;
		public byte[] ByteArray { get; set; } = new byte[0];

		public int IntValue { get; set; }
		public long LongValue { get; set; }
		public double DoubleValue { get; set; }
		public decimal DecimalValue { get; set; }
		public decimal DecimalLongName { get; set; }
		public float FloatValue { get; set; }
		public bool BoolValue { get; set; }
		public char CharValue { get; set; }
		public char[] CharArray { get; set; } = new char[0];
		public short ShortValue { get; set; }
		public ushort UShortValue { get; set; }
		public uint UIntValue { get; set; }
		public ulong ULongValue { get; set; }
		public sbyte SByteValue { get; set; }
		public byte ByteValue { get; set; }
		public DateTime DateValue { get; set; }
		public DateTime DateTimeValue { get; set; }
		public DateOnly DateOnlyValue { get; set; }
		public DateTimeOffset DateTimeOffsetValue { get; set; }
		public TimeSpan TimeSpanValue { get; set; }
		public TimeOnly TimeOnlyValue { get; set; }
		public int? NullableIntValue { get; set; }
		public IEnumerable<int?> NullableIntGeneric { get; set; } = [];
		public string? NullableStringValue { get; set; }
		public string?[] NullableStringArray { get; set; } = new string[0];
		public Guid GuidValue { get; set; }
		public MyEnum EnumValue { get; set; }
		public MyStringEnum StringEnumValue { get; set; }
		public int[] IntArray { get; set; } = new int[0];
		public IEnumerable Enumerable { get; set; } = new int[0];
		public IEnumerable<int> GenericIntEnumerable { get; set; } = new int[0];

		[JsonIgnore]
		public string Ignored { get; set; } = string.Empty;
	}
}