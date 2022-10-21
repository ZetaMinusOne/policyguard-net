using Microsoft.AspNetCore.Http;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    public class PolicyGuardMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PolicyGuard _policyGuard;

        public PolicyGuardMiddleware(RequestDelegate next, PolicyGuard policyGuard)
        {
            _next = next;
            _policyGuard = policyGuard;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            await _policyGuard.SetPolicyGuardHeaders(context).ConfigureAwait(false);
        }
    }

}