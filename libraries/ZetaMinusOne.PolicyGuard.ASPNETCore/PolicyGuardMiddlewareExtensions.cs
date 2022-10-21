using Microsoft.AspNetCore.Builder;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    public static class PolicyGuardMiddlewareExtensions
    { 
        public static IApplicationBuilder UsePolicyGuardHeaders(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PolicyGuardMiddleware>();
        }
    }

}