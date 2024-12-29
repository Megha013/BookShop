using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.CodeDom;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace BookShop.Models
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session,string key,T value)
        {
            session.SetString(key,JsonSerializer.Serialize(value));
        }
        public static T Get<T>(this ISession session, string key)
        { 
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize <T>( value);
        }
    }
}
