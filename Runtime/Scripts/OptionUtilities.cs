using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Net;
using UnityEngine;

namespace SafedGames.Options
{
    public static class OptionUtilities
    {
        [Pure]
        public static Option<int> ParseInt(string value) =>
            ParseInt(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<int> ParseInt(string value, CultureInfo cultureInfo) =>
            int.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<int>.Of(res) : new None<int>();

        [Pure]
        public static Option<long> ParseLong(string value) =>
            ParseLong(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<long> ParseLong(string value, CultureInfo cultureInfo) =>
            long.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<long>.Of(res) : new None<long>();

        [Pure]
        public static Option<short> ParseShort(string value) =>
            ParseShort(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<short> ParseShort(string value, CultureInfo cultureInfo) =>
            short.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<short>.Of(res) : new None<short>();

        [Pure]
        public static Option<byte> ParseByte(string value) =>
            ParseByte(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<byte> ParseByte(string value, CultureInfo cultureInfo) =>
            byte.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<byte>.Of(res) : new None<byte>();

        [Pure]
        public static Option<char> ParseChar(string value) =>
            char.TryParse(value, out var res) ? Some<char>.Of(res) : new None<char>();

        [Pure]
        public static Option<sbyte> ParseSByte(string value) =>
            ParseSByte(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<sbyte> ParseSByte(string value, CultureInfo cultureInfo) =>
            sbyte.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<sbyte>.Of(res) : new None<sbyte>();

        [Pure]
        public static Option<ulong> ParseULong(string value) =>
            ParseULong(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<ulong> ParseULong(string value, CultureInfo cultureInfo) =>
            ulong.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<ulong>.Of(res) : new None<ulong>();

        [Pure]
        public static Option<uint> ParseUInt(string value) =>
            ParseUInt(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<uint> ParseUInt(string value, CultureInfo cultureInfo) =>
            uint.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<uint>.Of(res) : new None<uint>();

        [Pure]
        public static Option<ushort> ParseUShort(string value) =>
            ParseUShort(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<ushort> ParseUShort(string value, CultureInfo cultureInfo) =>
            ushort.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<ushort>.Of(res) : new None<ushort>();

        [Pure]
        public static Option<float> ParseFloat(string value) =>
            ParseFloat(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<float> ParseFloat(string value, CultureInfo cultureInfo) =>
            float.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<float>.Of(res) : new None<float>();

        [Pure]
        public static Option<double> ParseDouble(string value) =>
            ParseDouble(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<double> ParseDouble(string value, CultureInfo cultureInfo) =>
            double.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<double>.Of(res) : new None<double>();

        [Pure]
        public static Option<decimal> ParseDecimal(string value) =>
            ParseDecimal(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<decimal> ParseDecimal(string value, CultureInfo cultureInfo) =>
            decimal.TryParse(value, NumberStyles.Any, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<decimal>.Of(res) : new None<decimal>();

        [Pure]
        public static Option<bool> ParseBool(string value) =>
            bool.TryParse(value, out var res) ? Some<bool>.Of(res) : new None<bool>();

        [Pure]
        public static Option<Guid> ParseGuid(string value) =>
            Guid.TryParse(value, out var res) ? Some<Guid>.Of(res) : new None<Guid>();

        [Pure]
        public static Option<DateTime> ParseDateTime(string value) =>
            ParseDateTime(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<DateTime> ParseDateTime(string value, CultureInfo cultureInfo) =>
            DateTime.TryParse(value, cultureInfo ?? CultureInfo.InvariantCulture, DateTimeStyles.None , out var res) ? Some<DateTime>.Of(res) : new None<DateTime>();

        [Pure]
        public static Option<DateTimeOffset> ParseDateTimeOffset(string value) =>
            ParseDateTimeOffset(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<DateTimeOffset> ParseDateTimeOffset(string value, CultureInfo cultureInfo) =>
            DateTimeOffset.TryParse(value, cultureInfo ?? CultureInfo.InvariantCulture, DateTimeStyles.None, out var res) ? Some<DateTimeOffset>.Of(res) : new None<DateTimeOffset>();

        [Pure]
        public static Option<TimeSpan> ParseTimeSpan(string value) =>
            ParseTimeSpan(value, CultureInfo.CurrentCulture);

        [Pure]
        public static Option<TimeSpan> ParseTimeSpan(string value, CultureInfo cultureInfo) =>
            TimeSpan.TryParse(value, cultureInfo ?? CultureInfo.InvariantCulture, out var res) ? Some<TimeSpan>.Of(res) : new None<TimeSpan>();

        [Pure]
        public static Option<T> ParseEnum<T>(string value) where T : struct =>
            Enum.TryParse<T>(value, out var res) ? Some<T>.Of(res) : new None<T>();

        [Pure]
        public static Option<T> ParseEnumIgnoreCase<T>(string value) where T : struct =>
            Enum.TryParse<T>(value, true, out var res) ? Some<T>.Of(res) : new None<T>();

        [Pure]
        public static Option<IPAddress> ParseIpAddress(string value) =>
            IPAddress.TryParse(value, out var res) ? Some<IPAddress>.Of(res) : new None<IPAddress>();

        public static Option<T> Next<T>(this Stack<T> stack) =>
            stack.TryPop(out var result) ? Some<T>.Of(result) : new None<T>();

        public static Option<T> Preview<T>(this Stack<T> stack) =>
            stack.TryPeek(out var result) ? Some<T>.Of(result) : new None<T>();

        public static Option<T> Next<T>(this Queue<T> queue) =>
            queue.TryDequeue(out var result) ? Some<T>.Of(result) : new None<T>();

        public static Option<T> Preview<T>(this Queue<T> queue) =>
            queue.TryPeek(out var result) ? Some<T>.Of(result) : new None<T>();

        public static Option<GameObject> FindGameObject(string name) =>
            Option<GameObject>.AutoOption(GameObject.Find(name));

        public static Option<GameObject> FindGameObjectWithTag(string tag)
        {
            try
            {
                return Option<GameObject>.AutoOption(GameObject.FindGameObjectWithTag(tag));
            }
            catch (UnityException e)
            {
                Debug.LogError(e.Message);
                return new None<GameObject>();
            }
        }

        public static Option<GameObject[]> FindGameObjectsWithTag(string tag)
        {
            try
            {
                return Option<GameObject[]>.AutoOption(GameObject.FindGameObjectsWithTag(tag));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return new None<GameObject[]>();
            }
        }

        public static Option<Camera> FindMainCamera() =>
            Option<Camera>.AutoOption(Camera.main);

        public static Option<T> FindComponent<T>(this GameObject target) where T : Component =>
            target.TryGetComponent<T>(out var found) ? Some<T>.Of(found) : new None<T>();

        public static Option<T> FindComponent<T>(this Component target) where T : Component =>
            target.gameObject.FindComponent<T>();
    }
}
