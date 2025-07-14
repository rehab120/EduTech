using System.Collections.Concurrent;
using EduTech.Repositry;

namespace EduTech.Extension
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = IdentityRepositry._blacklistedTokens;

        public TokenBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null && _blacklistedTokens.ContainsKey(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token has been invalidated. Please log in again.");
                return;
            }

            await _next(context);
        }
    }
}
