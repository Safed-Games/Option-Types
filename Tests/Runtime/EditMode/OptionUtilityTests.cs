using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using NUnit.Framework;
using static SafedGames.Options.OptionUtilities;

namespace SafedGames.Options.Tests.Runtime.EditMode
{
    public class OptionUtilityTests
    {
        private enum TestEnum
        {
            First,
            Second,
            Third,
        }

        private static string[] _validInts = { int.MinValue.ToString(), "1", "52", "752", "5743", "1234567", int.MaxValue.ToString() };
        private static string[] _validLongs = { long.MinValue.ToString(), "1", "52", "752", "5743", "1234567", long.MaxValue.ToString() };
        private static string[] _validShorts = { short.MinValue.ToString(), "1", "52", "752", "5743", short.MaxValue.ToString() };
        private static string[] _validBytes = { byte.MinValue.ToString(), "1", "52", "200", byte.MaxValue.ToString() };
        private static string[] _validChars = { "a", "9", "_", " " };
        private static string[] _validSBytes = { sbyte.MinValue.ToString(), "-1", "1", "52", "100", sbyte.MaxValue.ToString() };
        private static string[] _validUInts = { uint.MinValue.ToString(), "1", "52", "752", "5743", "1234567", uint.MaxValue.ToString() };
        private static string[] _validULongs = { ulong.MinValue.ToString(), "1", "52", "752", "5743", "1234567", ulong.MaxValue.ToString() };
        private static string[] _validUShorts = { ushort.MinValue.ToString(), "1", "52", "752", "5743", ushort.MaxValue.ToString() };
        private static string[] _notNumbers = { "hello", "test", "number" };
        private static string[] _validDecimalNumbers = { decimal.MinValue.ToString(CultureInfo.InvariantCulture), "0.0", "100.25", "567.45485284", "-27.05", "70", ".1", "-.5", decimal.MaxValue.ToString(CultureInfo.InvariantCulture) };
        private static string[] _validBooleans = { "True", "true", "TrUe", "TRUE", "False", "false", "FaLsE", "FALSE" };
        private static string[] _validGuids = { "00000000000000000000000000000000", "00000000-0000-0000-0000-000000000000" };
        private static string[] _validDateTimes = { "June 05 1990", "09.03.2022", "03/12/87" };
        private static string[] _validDateTimeOffsets = { "05/01/2008", "11:36 PM", "05/01/2008 +1:00", "Thu May 01, 2008" };
        private static string[] _validTimeSpans = { "6", "6:12", "6:12:14", "6:12:14:45", "6.12:14:45", "6:12:14:45.3448" };
        private static string[] _validEnums = { "First", "Second", "Third" };
        private static string[] _incorrectCaseEnums = { "First", "Second", "Third", "first", "sEcOnD", "THIRD" };
        private static string[] _validIpAddresses = { "127.0.0.1", "0:0:0:0:0:0:0:1", "127001" };
        private static string[] _invalidIpAddresses = { "260.0.0.1", "0:0:0:0:0:0:0:GG" };

        [Test]
        public void ParseInt_ValidInt_ReturnsSome([ValueSource(nameof(_validInts))] string value)
        {
            Assert.That(ParseInt(value).IsSome);
        }

        [Test]
        public void ParseInt_ValidInt_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validInts))] string value)
        {
            Assert.That(ParseInt(value).OrThrow(), Is.EqualTo(int.Parse(value)));
        }

        [Test]
        public void ParseInt_InvalidInt_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseInt(value).IsNone);
        }

        [Test]
        public void ParseInt_TooLargeInt_ReturnsNone()
        {
            Assert.That(ParseInt(int.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseLong_ValidLong_ReturnsSome([ValueSource(nameof(_validLongs))] string value)
        {
            Assert.That(ParseLong(value).IsSome);
        }

        [Test]
        public void ParseLong_ValidLong_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validLongs))] string value)
        {
            Assert.That(ParseLong(value).OrThrow(), Is.EqualTo(long.Parse(value)));
        }

        [Test]
        public void ParseLong_InvalidLong_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseLong(value).IsNone);
        }

        [Test]
        public void ParseLong_TooLargeLong_ReturnsNone()
        {
            Assert.That(ParseLong(long.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseShort_ValidShort_ReturnsSome([ValueSource(nameof(_validShorts))] string value)
        {
            Assert.That(ParseShort(value).IsSome);
        }

        [Test]
        public void ParseShort_ValidShort_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validShorts))] string value)
        {
            Assert.That(ParseShort(value).OrThrow(), Is.EqualTo(short.Parse(value)));
        }

        [Test]
        public void ParseShort_InvalidShort_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseShort(value).IsNone);
        }

        [Test]
        public void ParseShort_TooLargeShort_ReturnsNone()
        {
            Assert.That(ParseShort(short.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseByte_ValidByte_ReturnsSome([ValueSource(nameof(_validBytes))] string value)
        {
            Assert.That(ParseByte(value).IsSome);
        }

        [Test]
        public void ParseByte_ValidByte_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validBytes))] string value)
        {
            Assert.That(ParseByte(value).OrThrow(), Is.EqualTo(byte.Parse(value)));
        }

        [Test]
        public void ParseByte_InvalidByte_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseByte(value).IsNone);
        }

        [Test]
        public void ParseByte_TooLargeByte_ReturnsNone()
        {
            Assert.That(ParseByte(byte.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseChar_ValidChar_ReturnsSome([ValueSource(nameof(_validChars))] string value)
        {
            Assert.That(ParseChar(value).IsSome);
        }

        [Test]
        public void ParseChar_ValidChar_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validChars))] string value)
        {
            Assert.That(ParseChar(value).OrThrow(), Is.EqualTo(char.Parse(value)));
        }

        [Test]
        public void ParseChar_InvalidChar_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseChar(value).IsNone);
        }

        [Test]
        public void ParseSByte_ValidSByte_ReturnsSome([ValueSource(nameof(_validSBytes))] string value)
        {
            Assert.That(ParseSByte(value).IsSome);
        }

        [Test]
        public void ParseSByte_ValidSByte_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validSBytes))] string value)
        {
            Assert.That(ParseSByte(value).OrThrow(), Is.EqualTo(sbyte.Parse(value)));
        }

        [Test]
        public void ParseSByte_InvalidSByte_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseSByte(value).IsNone);
        }

        [Test]
        public void ParseSByte_TooLargeSByte_ReturnsNone()
        {
            Assert.That(ParseSByte(sbyte.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseUInt_ValidUInt_ReturnsSome([ValueSource(nameof(_validUInts))] string value)
        {
            Assert.That(ParseUInt(value).IsSome);
        }

        [Test]
        public void ParseUInt_ValidUInt_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validUInts))] string value)
        {
            Assert.That(ParseUInt(value).OrThrow(), Is.EqualTo(uint.Parse(value)));
        }

        [Test]
        public void ParseUInt_InvalidUInt_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseUInt(value).IsNone);
        }

        [Test]
        public void ParseUInt_TooLargeUInt_ReturnsNone()
        {
            Assert.That(ParseUInt(uint.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseULong_ValidULong_ReturnsSome([ValueSource(nameof(_validULongs))] string value)
        {
            Assert.That(ParseULong(value).IsSome);
        }

        [Test]
        public void ParseULong_ValidULong_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validULongs))] string value)
        {
            Assert.That(ParseULong(value).OrThrow(), Is.EqualTo(ulong.Parse(value)));
        }

        [Test]
        public void ParseULong_InvalidULong_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseUInt(value).IsNone);
        }

        [Test]
        public void ParseULong_TooLargeULong_ReturnsNone()
        {
            Assert.That(ParseULong(ulong.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseUShort_ValidUShort_ReturnsSome([ValueSource(nameof(_validUShorts))] string value)
        {
            Assert.That(ParseUShort(value).IsSome);
        }

        [Test]
        public void ParseUShort_ValidUShort_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validUShorts))] string value)
        {
            Assert.That(ParseUShort(value).OrThrow(), Is.EqualTo(ushort.Parse(value)));
        }

        [Test]
        public void ParseUShort_InvalidUShort_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseUShort(value).IsNone);
        }

        [Test]
        public void ParseUShort_TooLargeUShort_ReturnsNone()
        {
            Assert.That(ParseUShort(ushort.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseFloat_ValidFloat_ReturnsSome([ValueSource(nameof(_validDecimalNumbers))] string value)
        {
            Assert.That(ParseFloat(value).IsSome);
        }

        [Test]
        public void ParseFloat_ValidFloat_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validDecimalNumbers))] string value)
        {
            Assert.That(ParseFloat(value).OrThrow(), Is.EqualTo(float.Parse(value)));
        }

        [Test]
        public void ParseFloat_InvalidFloat_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseFloat(value).IsNone);
        }

        [Test]
        public void ParseFloat_TooLargeFloat_ReturnsNone()
        {
            Assert.That(ParseFloat(float.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseDouble_ValidDouble_ReturnsSome([ValueSource(nameof(_validDecimalNumbers))] string value)
        {
            Assert.That(ParseDouble(value).IsSome);
        }

        [Test]
        public void ParseDouble_ValidDouble_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validDecimalNumbers))] string value)
        {
            Assert.That(ParseDouble(value).OrThrow(), Is.EqualTo(double.Parse(value)));
        }

        [Test]
        public void ParseDouble_InvalidDouble_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseDouble(value).IsNone);
        }

        [Test]
        public void ParseDouble_TooLargeDouble_ReturnsNone()
        {
            Assert.That(ParseDouble(double.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseDecimal_ValidDecimal_ReturnsSome([ValueSource(nameof(_validDecimalNumbers))] string value)
        {
            Assert.That(ParseDecimal(value).IsSome);
        }

        [Test]
        public void ParseDecimal_ValidDecimal_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validDecimalNumbers))] string value)
        {
            Assert.That(ParseDecimal(value).OrThrow(), Is.EqualTo(decimal.Parse(value)));
        }

        [Test]
        public void ParseDecimal_InvalidDecimal_ReturnsNone([ValueSource(nameof(_notNumbers))] string value)
        {
            Assert.That(ParseDecimal(value).IsNone);
        }

        [Test]
        public void ParseDecimal_TooLargeDecimal_ReturnsNone()
        {
            Assert.That(ParseDecimal(decimal.MaxValue + "1").IsNone);
        }

        [Test]
        public void ParseBool_ValidBool_ReturnsSome([ValueSource(nameof(_validBooleans))] string value)
        {
            Assert.That(ParseBool(value).IsSome);
        }

        [Test]
        public void ParseBool_ValidBool_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validBooleans))] string value)
        {
            Assert.That(ParseBool(value).OrThrow(), Is.EqualTo(bool.Parse(value)));
        }

        [Test]
        public void ParseBool_InvalidBool_ReturnsNone()
        {
            Assert.That(ParseBool("0").IsNone);
        }

        [Test]
        public void ParseGUID_ValidGUID_ReturnsSome([ValueSource(nameof(_validGuids))] string value)
        {
            Assert.That(ParseGuid(value).IsSome);
        }

        [Test]
        public void ParseGUID_ValidGUID_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validGuids))] string value)
        {
            Assert.That(ParseGuid(value).OrThrow(), Is.EqualTo(Guid.Parse(value)));
        }

        [Test]
        public void ParseGUID_InvalidGUID_ReturnsNone()
        {
            Assert.That(ParseGuid("00000000-0000-0000-0000-00000000000z").IsNone);
        }

        [Test]
        public void ParseDateTime_ValidDateTime_ReturnsSome([ValueSource(nameof(_validDateTimes))] string value)
        {
            Assert.That(ParseDateTime(value).IsSome);
        }

        [Test]
        public void ParseDateTime_ValidDateTime_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validDateTimes))] string value)
        {
            Assert.That(ParseDateTime(value).OrThrow(), Is.EqualTo(DateTime.Parse(value)));
        }

        [Test]
        public void ParseDateTime_InvalidDateTime_ReturnsNone()
        {
            Assert.That(ParseDateTime("111111111").IsNone);
        }

        [Test]
        public void ParseDateTime_DateTimeSlightlyIncorrect_ReturnsNone()
        {
            Assert.That(ParseDateTime("May 40 2020").IsNone);
        }

        [Test]
        public void ParseDateTimeOffset_ValidDateTimeOffset_ReturnsSome([ValueSource(nameof(_validDateTimeOffsets))] string value)
        {
            Assert.That(ParseDateTimeOffset(value).IsSome);
        }

        [Test]
        public void ParseDateTimeOffset_ValidDateTimeOffset_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validDateTimeOffsets))] string value)
        {
            Assert.That(ParseDateTimeOffset(value).OrThrow(), Is.EqualTo(DateTimeOffset.Parse(value)));
        }

        [Test]
        public void ParseDateTimeOffset_InvalidDateTimeOffset_ReturnsNone()
        {
            Assert.That(ParseDateTimeOffset("111111111").IsNone);
        }

        [Test]
        public void ParseDateTimeOffset_DateTimeOffsetSlightlyIncorrect_ReturnsNone()
        {
            Assert.That(ParseDateTimeOffset("25:71 PM").IsNone);
        }

        [Test]
        public void ParseTimeSpan_ValidTimeSpan_ReturnsSome([ValueSource(nameof(_validTimeSpans))] string value)
        {
            Assert.That(ParseTimeSpan(value).IsSome);
        }

        [Test]
        public void ParseTimeSpan_ValidTimeSpan_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validTimeSpans))] string value)
        {
            Assert.That(ParseTimeSpan(value).OrThrow(), Is.EqualTo(TimeSpan.Parse(value)));
        }

        [Test]
        public void ParseTimeSpan_InvalidTimeSpan_ReturnsNone()
        {
            Assert.That(ParseTimeSpan("111111111").IsNone);
        }

        [Test]
        public void ParseTimeSpan_TimeSpanSlightlyIncorrect_ReturnsNone()
        {
            Assert.That(ParseTimeSpan("6:34:14:45").IsNone);
        }

        [Test]
        public void ParseEnum_ValidEnum_ReturnsSome([ValueSource(nameof(_validEnums))] string value)
        {
            Assert.That(ParseEnum<TestEnum>(value).IsSome);
        }

        [Test]
        public void ParseEnum_ValidEnum_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validEnums))] string value)
        {
            Assert.That(ParseEnum<TestEnum>(value).OrThrow(), Is.EqualTo(Enum.Parse<TestEnum>(value)));
        }

        [Test]
        public void ParseEnum_InvalidEnum_ReturnsNone()
        {
            Assert.That(ParseEnum<TestEnum>("first").IsNone);
        }

        [Test]
        public void ParseEnumIgnoreCase_ValidEnum_ReturnsSome([ValueSource(nameof(_incorrectCaseEnums))] string value)
        {
            Assert.That(ParseEnumIgnoreCase<TestEnum>(value).IsSome);
        }

        [Test]
        public void ParseEnumIgnoreCase_ValidEnum_ReturnsSomeOfCorrectValue([ValueSource(nameof(_incorrectCaseEnums))] string value)
        {
            Assert.That(ParseEnumIgnoreCase<TestEnum>(value).OrThrow(), Is.EqualTo(Enum.Parse<TestEnum>(value, true)));
        }

        [Test]
        public void ParseEnumIgnoreCase_InvalidEnum_ReturnsNone()
        {
            Assert.That(ParseEnumIgnoreCase<TestEnum>("1st").IsNone);
        }

        [Test]
        public void ParseIpAddress_ValidIpAddress_ReturnsSome([ValueSource(nameof(_validIpAddresses))] string value)
        {
            Assert.That(ParseIpAddress(value).IsSome);
        }

        [Test]
        public void ParseIpAddress_ValidIpAddress_ReturnsSomeOfCorrectValue([ValueSource(nameof(_validIpAddresses))] string value)
        {
            Assert.That(ParseIpAddress(value).OrThrow(), Is.EqualTo(IPAddress.Parse(value)));
        }

        [Test]
        public void ParseIpAddress_InvalidIpAddress_ReturnsNone()
        {
            Assert.That(ParseIpAddress("11111111111").IsNone);
        }

        [Test]
        public void ParseIpAddress_IpAddressSlightlyIncorrect_ReturnsNone([ValueSource(nameof(_invalidIpAddresses))] string value)
        {
            Assert.That(ParseIpAddress(value).IsNone);
        }

        [Test]
        public void StackNext_StackHasValues_ReturnsSome()
        {
            var stackUnderTest = new Stack<int>();
            stackUnderTest.Push(1);

            Assert.True(stackUnderTest.Next().IsSome);
        }

        [Test]
        public void StackNext_StackHasNoValues_ReturnsNone()
        {
            var stackUnderTest = new Stack<int>();

            Assert.True(stackUnderTest.Next().IsNone);
        }

        [Test]
        public void StackNext_StackHasValues_ReturnsSomeOfCorrectValue()
        {
            var stackUnderTest = new Stack<int>();
            const int testValue = 12;
            stackUnderTest.Push(testValue);

            Assert.That(stackUnderTest.Next().OrThrow(), Is.EqualTo(testValue));
        }

        [Test]
        public void StackNext_StackHasMultipleValues_ReturnsSomeOfCorrectValue()
        {
            var stackUnderTest = new Stack<int>();
            const int testValue = 12;
            const int additionValue = 13;
            stackUnderTest.Push(additionValue);
            stackUnderTest.Push(testValue);

            Assert.That(stackUnderTest.Next().OrThrow(), Is.EqualTo(testValue));
            Assert.That(stackUnderTest.Next().OrThrow(), Is.EqualTo(additionValue));
        }

        [Test]
        public void StackNext_StackHasSingleValue_ValueIsPopped()
        {
            var stackUnderTest = new Stack<int>();
            const int testValue = 12;
            stackUnderTest.Push(testValue);

            _ = stackUnderTest.Next();

            Assert.True(stackUnderTest.Next().IsNone);
        }

        [Test]
        public void StackPreview_StackHasValues_ReturnsSome()
        {
            var stackUnderTest = new Stack<int>();
            stackUnderTest.Push(1);

            Assert.True(stackUnderTest.Preview().IsSome);
        }

        [Test]
        public void StackPreview_StackHasNoValues_ReturnsNone()
        {
            var stackUnderTest = new Stack<int>();

            Assert.True(stackUnderTest.Next().IsNone);
        }

        [Test]
        public void StackPreview_StackHasValues_ReturnsSomeOfCorrectValue()
        {
            var stackUnderTest = new Stack<int>();
            const int testValue = 12;
            stackUnderTest.Push(testValue);

            Assert.That(stackUnderTest.Preview().OrThrow(), Is.EqualTo(testValue));
        }

        [Test]
        public void StackPreview_StackHasMultipleValues_ReturnsSomeOfCorrectValue()
        {
            var stackUnderTest = new Stack<int>();
            const int testValue = 12;
            const int additionValue = 13;
            stackUnderTest.Push(additionValue);
            stackUnderTest.Push(testValue);

            Assert.That(stackUnderTest.Preview().OrThrow(), Is.EqualTo(testValue));
        }

        [Test]
        public void QueueNext_QueueHasValues_ReturnsSome()
        {
            var queueUnderTest = new Queue<int>();
            queueUnderTest.Enqueue(1);

            Assert.True(queueUnderTest.Next().IsSome);
        }

        [Test]
        public void QueueNext_QueueHasNoValues_ReturnsNone()
        {
            var queueUnderTest = new Queue<int>();

            Assert.True(queueUnderTest.Next().IsNone);
        }

        [Test]
        public void QueueNext_QueueHasValues_ReturnsSomeOfCorrectValue()
        {
            var queueUnderTest = new Queue<int>();
            const int testValue = 12;
            queueUnderTest.Enqueue(testValue);

            Assert.That(queueUnderTest.Next().OrThrow(), Is.EqualTo(testValue));
        }

        [Test]
        public void QueueNext_QueueHasMultipleValues_ReturnsSomeOfCorrectValue()
        {
            var queueUnderTest = new Queue<int>();
            const int testValue = 12;
            const int additionValue = 13;
            queueUnderTest.Enqueue(testValue);
            queueUnderTest.Enqueue(additionValue);

            Assert.That(queueUnderTest.Next().OrThrow(), Is.EqualTo(testValue));
            Assert.That(queueUnderTest.Next().OrThrow(), Is.EqualTo(additionValue));
        }

        [Test]
        public void QueueNext_QueueHasSingleValue_ValueIsDequeued()
        {
            var queueUnderTest = new Queue<int>();
            const int testValue = 12;
            queueUnderTest.Enqueue(testValue);

            _ = queueUnderTest.Next();

            Assert.True(queueUnderTest.Next().IsNone);
        }

        [Test]
        public void QueuePreview_QueueHasValues_ReturnsSome()
        {
            var queueUnderTest = new Queue<int>();
            queueUnderTest.Enqueue(1);

            Assert.True(queueUnderTest.Preview().IsSome);
        }

        [Test]
        public void QueuePreview_QueueHasNoValues_ReturnsNone()
        {
            var queueUnderTest = new Queue<int>();

            Assert.True(queueUnderTest.Next().IsNone);
        }

        [Test]
        public void QueuePreview_QueueHasValues_ReturnsSomeOfCorrectValue()
        {
            var queueUnderTest = new Queue<int>();
            const int testValue = 12;
            queueUnderTest.Enqueue(testValue);

            Assert.That(queueUnderTest.Preview().OrThrow(), Is.EqualTo(testValue));
        }

        [Test]
        public void QueuePreview_QueueHasMultipleValues_ReturnsSomeOfCorrectValue()
        {
            var queueUnderTest = new Queue<int>();
            const int testValue = 12;
            const int additionValue = 13;
            queueUnderTest.Enqueue(testValue);
            queueUnderTest.Enqueue(additionValue);

            Assert.That(queueUnderTest.Preview().OrThrow(), Is.EqualTo(testValue));
        }
    }
}
