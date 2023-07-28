using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using UnityEngine;

#if HAS_RAYCAST_CONFIG
using SafedGames.Utility;
#endif

namespace SafedGames.Options
{
    public static class OptionUtilities
    {
        [Pure]
        public static Option<int> ParseInt(string value) =>
            int.TryParse(value, out var res) ? Some<int>.Of(res) : new None<int>();

        [Pure]
        public static Option<long> ParseLong(string value) =>
            long.TryParse(value, out var res) ? Some<long>.Of(res) : new None<long>();

        [Pure]
        public static Option<short> ParseShort(string value) =>
            short.TryParse(value, out var res) ? Some<short>.Of(res) : new None<short>();

        [Pure]
        public static Option<byte> ParseByte(string value) =>
            byte.TryParse(value, out var res) ? Some<byte>.Of(res) : new None<byte>();

        [Pure]
        public static Option<char> ParseChar(string value) =>
            char.TryParse(value, out var res) ? Some<char>.Of(res) : new None<char>();

        [Pure]
        public static Option<sbyte> ParseSByte(string value) =>
            sbyte.TryParse(value, out var res) ? Some<sbyte>.Of(res) : new None<sbyte>();

        [Pure]
        public static Option<ulong> ParseULong(string value) =>
            ulong.TryParse(value, out var res) ? Some<ulong>.Of(res) : new None<ulong>();

        [Pure]
        public static Option<uint> ParseUInt(string value) =>
            uint.TryParse(value, out var res) ? Some<uint>.Of(res) : new None<uint>();

        [Pure]
        public static Option<ushort> ParseUShort(string value) =>
            ushort.TryParse(value, out var res) ? Some<ushort>.Of(res) : new None<ushort>();

        [Pure]
        public static Option<float> ParseFloat(string value) =>
            float.TryParse(value, out var res) ? Some<float>.Of(res) : new None<float>();

        [Pure]
        public static Option<double> ParseDouble(string value) =>
            double.TryParse(value, out var res) ? Some<double>.Of(res) : new None<double>();

        [Pure]
        public static Option<decimal> ParseDecimal(string value) =>
            decimal.TryParse(value, out var res) ? Some<decimal>.Of(res) : new None<decimal>();

        [Pure]
        public static Option<bool> ParseBool(string value) =>
            bool.TryParse(value, out var res) ? Some<bool>.Of(res) : new None<bool>();

        [Pure]
        public static Option<Guid> ParseGuid(string value) =>
            Guid.TryParse(value, out var res) ? Some<Guid>.Of(res) : new None<Guid>();

        [Pure]
        public static Option<DateTime> ParseDateTime(string value) =>
            DateTime.TryParse(value, out var res) ? Some<DateTime>.Of(res) : new None<DateTime>();

        [Pure]
        public static Option<DateTimeOffset> ParseDateTimeOffset(string value) =>
            DateTimeOffset.TryParse(value, out var res) ? Some<DateTimeOffset>.Of(res) : new None<DateTimeOffset>();

        [Pure]
        public static Option<TimeSpan> ParseTimeSpan(string value) =>
            TimeSpan.TryParse(value, out var res) ? Some<TimeSpan>.Of(res) : new None<TimeSpan>();

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

#if UNITY_2021_1_OR_NEWER
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

        public static Option<GameObject[]> FindGameObjectsWithTag(string tag) =>
            Option<GameObject[]>.AutoOption(GameObject.FindGameObjectsWithTag(tag));

        public static Option<Camera> GetMainCamera() =>
            Option<Camera>.AutoOption(Camera.main);

        public static Option<T> FindComponent<T>(this Component target) where T : Component =>
            target.TryGetComponent<T>(out var found) ? Some<T>.Of(found) : new None<T>();

#if HAS_RAYCAST_CONFIG
        public static Option<RaycastHit> Raycast(RaycastConfig config) =>
            Physics.Raycast(
                config.Ray,
                out var hit,
                config.MaxDistance,
                config.LayerMask,
                config.QueryTriggerInteraction
            )
                ? Some<RaycastHit>.Of(hit)
                : new None<RaycastHit>();

        public static Option<RaycastHit[]> RaycastAll(RaycastConfig config)
        {
            Option<RaycastHit>.AutoOption(
                Physics.RaycastAll(
                    config.Ray,
                    config.MaxDistance,
                    config.LayerMask,
                    config.QueryTriggerInteraction
                ));
        }
#endif
#endif
    }
}
