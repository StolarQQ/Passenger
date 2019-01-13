using Microsoft.AspNetCore.Builder;

namespace Passenger.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMyExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}