using System;
using System.Collections;
using System.Diagnostics.Contracts;

namespace SafedGames.Options
{
    public abstract class Option<T>
    {
        protected T Value;

        public void Match(Action<T> ifSome, Action ifNone)
        {
            if (IsSome)
            {
                ifSome?.Invoke(Value);
            }
            else if (IsNone)
            {
                ifNone?.Invoke();
            }
        }

        [Pure]
        public bool IsSome => this is Some<T>;

        [Pure]
        public bool IsNone => this is None<T>;

        [Pure]
        public T Or(T ifNone) => IsSome ? Value : ifNone;

        [Pure]
        public T OrDefault() => IsSome ? Value : default;

        [Pure]
        public T OrThrow() => IsSome ? Value : throw new AccessingNoneException();

        [Pure]
        public T OrThrow(string message) => IsSome ? Value : throw new AccessingNoneException(message);

        internal static Option<T> AutoOption(T original) =>
            original switch
            {
                UnityEngine.Object unityOriginal => unityOriginal ? Some<T>.Of(original) : new None<T>(),
                ICollection collection => collection.Count != 0 ? Some<T>.Of(original) : new None<T>(),
                not null => Some<T>.Of(original),
                _ => new None<T>()
            };
    }

    public sealed class Some<T> : Option<T>
    {
        private Some(T value) => Value = value;

        public static Some<T> Of(T value) => new(value);
    }

    public sealed class None<T> : Option<T>
    {
        public static None<T> Object => new();
    }
}
