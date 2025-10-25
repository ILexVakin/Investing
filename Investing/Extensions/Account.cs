using Investing.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Investing.Extensions
{
    public static class Account
    {
        public static void SetUser(this ISession session, Credentials user)
        {
            session.SetObject("CurrentUser", user);
            session.SetBool("IsAuthenticated", true);
        }

        public static User GetUser(this ISession session)
        {
            return session.GetObject<User>("CurrentUser");
        }

        public static bool IsAuthenticated(this ISession session)
        {
            return session.GetBool("IsAuthenticated") ?? false;
        }

        public static void Logout(this ISession session)
        {
            session.Remove("CurrentUser");
            session.Remove("IsAuthenticated");
        }
    }
}
