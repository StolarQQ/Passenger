using System;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Extenstions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this IMemoryCache cache, Guid tokenid, JwtDto jwt)
            => cache.Set(GetJwtKey(tokenid), jwt, TimeSpan.FromSeconds(5));

        public static JwtDto GetJwt(this IMemoryCache cache, Guid tokenid)
            => cache.Get<JwtDto>(GetJwtKey(tokenid));

        // Create key for token
        private static string GetJwtKey(Guid tokenid)
            => $"jwt-{tokenid}";
    }
}