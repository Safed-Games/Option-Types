using System;

namespace SafedGames.Options
{
    public class AccessingNoneException : Exception
    {
        public AccessingNoneException(string message) : base(message)
        {

        }
    }
}
