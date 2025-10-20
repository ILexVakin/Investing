using System.ComponentModel;
using System;
using System.Reflection;

namespace Investing.Services
{
    public static class Translator
    {
        public static string DescriptionEnum(this Enum value)
        {
            var instrument = value.GetType().GetField(value.ToString());
            var attribute = instrument.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}
