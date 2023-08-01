using System;
using System.Collections;
using System.Diagnostics.Contracts;

namespace SafedGames.Options
{
    public abstract class Option<T>
    {
        protected T Value;

        public void Match(Action<T> some, Action none)
        {
            if (this is Some<T>)
            {
                some(Value);
            }
            else
            {
                none();
            }
        }

        [Pure]
        public T Or(T ifNone) => this is Some<T> ? Value : ifNone;

        [Pure]
        public T OrDefault() => this is Some<T> ? Value : default;

        [Pure]
        public T OrThrow(string message) => this is Some<T> ? Value : throw new AccessingNoneException(message);

        internal static Option<T> AutoOption(T original) =>
            original switch
            {
                UnityEngine.Object unityOriginal => unityOriginal ? Some<T>.Of(original) : new None<T>(),
                ICollection collection => collection.Count != 0 ? Some<T>.Of(original) : new None<T>(),
                not null => Some<T>.Of(original),
                _ => new None<T>()
            };

        public static explicit operator T(Option<T> option) => option.OrThrow("Attempting to access value of None.");
    }

    public sealed class Some<T> : Option<T>
    {
        private Some(T value) => Value = value;

        public static Option<T> Of(T value) => new Some<T>(value);
    }

    public sealed class None<T> : Option<T> { }
}
