using System;

namespace SafedGames.Options
{
    public sealed class AccessingNoneException : Exception
    {
        public AccessingNoneException() : base("Attempting to value of a None typed Option.") { }

        public AccessingNoneException(string message) : base(message) { }
    }
}
