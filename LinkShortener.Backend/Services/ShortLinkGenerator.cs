using System;

namespace LinkShortener.Resource.Services
{
    public static class ShortLinkGenerator
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
