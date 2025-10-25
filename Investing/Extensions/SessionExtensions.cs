using Investing.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace Investing.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string jsonString = JsonSerializer.Serialize(value);
            session.SetString(key, jsonString);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string jsonString = session.GetString(key);
            if (string.IsNullOrEmpty(jsonString))
                return default(T);

            return JsonSerializer.Deserialize<T>(jsonString);
        }
        public static void SetBool(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static bool? GetBool(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
                return null;

            return BitConverter.ToBoolean(data, 0);
        }
    }
}
