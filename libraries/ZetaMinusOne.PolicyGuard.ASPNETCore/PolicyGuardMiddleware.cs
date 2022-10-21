using Microsoft.AspNetCore.Http;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    /// <summary>
    /// Middleware for update Policy Headers on <see cref="HttpResponse.Headers"/>
    /// </summary>
    public class PolicyGuardMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PolicyGuard _policyGuard;

        public PolicyGuardMiddleware(
            RequestDelegate next, PolicyGuardCredential credential)
        {
            _next = next;
            string apikey = credential.ApiKey;
            HttpClient client = new ();
            _policyGuard =  new PolicyGuard(client, apikey);
        }

        /// <summary>
        /// Middleware main method for Update the 
        /// <see cref="HttpResponse.Headers"/> from <see cref="HttpContext"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await _policyGuard.SetPolicyGuardHeaders(context)
                .ConfigureAwait(false);
            await _next(context);
        }
    }

    /// <summary>
    /// Class for storing the ApiKey of your 
    /// Registered Domain in <seealso href="https://www.policyguard.io/"/>
    /// </summary>
    public class PolicyGuardCredential
    {
        /// <summary>
        /// Assigned Api Key for your Domain.
        /// Visit <seealso href="https://www.policyguard.io/"/> 
        /// to optain your Api Key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}