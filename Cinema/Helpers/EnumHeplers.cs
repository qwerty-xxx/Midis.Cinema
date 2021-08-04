using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Helpers
{
    public static class EnumHeplers
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}