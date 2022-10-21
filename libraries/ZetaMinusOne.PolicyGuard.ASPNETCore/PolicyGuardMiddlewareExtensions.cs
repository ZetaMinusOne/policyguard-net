using Microsoft.AspNetCore.Builder;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    public static class PolicyGuardMiddlewareExtensions
    {
        /// <summary>
        /// An extension method exposing the <see cref="PolicyGuardMiddleware"/> 
        /// through <see cref="IApplicationBuilder"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePolicyGuardMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PolicyGuardMiddleware>();
        }
    }
}