using Microsoft.AspNetCore.Builder;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    public static class PolicyGuardMiddleware
    { 
        public static IApplicationBuilder UsePolicyGuard(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PolicyGuard>();
        }
    }

}