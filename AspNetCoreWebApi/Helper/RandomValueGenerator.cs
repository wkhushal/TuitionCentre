using System;

namespace AspNetCoreWebApi.Helper
{
    internal static class RandomValueGenerator
    {
        internal static T RandomEnumValue<T>() where T : Enum
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }

        internal static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
