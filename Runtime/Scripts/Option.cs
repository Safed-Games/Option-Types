using System;
using System.Collections;
using System.Diagnostics.Contracts;

namespace SafedGames.Options
{
    public abstract class Option<T>
    {
        public void Match(Action<T> ifSome, Action ifNone)
        {
            if (this is Some<T> some)
            {
                ifSome?.Invoke(some.Value);
                return;
            }

            ifNone?.Invoke();
        }

        public TReturn Match<TReturn>(Func<T, TReturn> ifSome, Func<TReturn> ifNone)
        {
            if (ifSome is null)
            {
                throw new ArgumentNullException(nameof(ifSome));
            }

            if (ifNone is null)
            {
                throw new ArgumentNullException(nameof(ifNone));
            }

            if (this is Some<T> some)
            {
                return ifSome.Invoke(some.Value);
            }

            return ifNone.Invoke();
        }

        [Pure]
        public bool IsSome => this is Some<T>;

        [Pure]
        public bool IsNone => this is None<T>;

        [Pure]
        public T Or(T ifNone) => this is Some<T> some ? some.Value : ifNone;

        [Pure]
        public T OrDefault() => this is Some<T> some ? some.Value : default;

        [Pure]
        public T OrThrow() => this is Some<T> some ? some.Value : throw new AccessingNoneException();

        [Pure]
        public T OrThrow(string message) => this is Some<T> some ? some.Value : throw new AccessingNoneException(message);

        internal static Option<T> AutoOption(T original) =>
            original switch
            {
                UnityEngine.Object unityOriginal => unityOriginal ? Some<T>.Of(original) : None<T>.Object,
                ICollection collection => collection.Count != 0 ? Some<T>.Of(original) : None<T>.Object,
                not null => Some<T>.Of(original),
                _ => None<T>.Object
            };
    }

    public sealed class Some<T> : Option<T>
    {
        internal readonly T Value;

        private Some(T value) => Value = value;

        public static Some<T> Of(T value) => new(value);
    }

    public sealed class None<T> : Option<T>
    {
        private None() { }

        public static None<T> Object => new();
    }
}
