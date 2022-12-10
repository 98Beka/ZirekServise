using ZirekService.Interfaces;
using ZirekService.Services;

namespace ZirekService
{
    public class StatisticMiddleware
    {
        private readonly RequestDelegate _next;

        public StatisticMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, IVisitStatisticService visitStatisticService)
        {
            visitStatisticService.SetVisitStatisticEntity(httpContext);
            await _next(httpContext);
        }
    }

    public static class StatisticMiddlewareExtensions
    {
        public static IApplicationBuilder UseStatisticMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StatisticMiddleware>();
        }
    }
}
