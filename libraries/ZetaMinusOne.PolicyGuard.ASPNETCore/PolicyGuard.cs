using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    public class PolicyGuard
    {
        private readonly PolicyHeaders _headers;
        private readonly HttpClient _httpClient;
        private const string uri = "https://www.policyguard.io/api/csp/";
        private const int expiryTime = 10000; 
        private int lastTime = 0;
        private bool fetching;

        public PolicyGuard(HttpClient httpClient)
        {
            _headers = new();
            _httpClient = httpClient;
        }


        // Get Policy Headers
        public async Task<PolicyHeaders> GetPolicyHeadersAsync(string apikey)
        {
            string apiUrl = uri + apikey ?? "";
            var res = await _httpClient.GetAsync(apiUrl).ConfigureAwait(false);
            return res.IsSuccessStatusCode ? new PolicyHeaders() : _headers;
        }

        // Get Policy Expiration
        // Is Policy Expired
        // Update Policy Headers
        // Set Policy Headers
        // With Policy Headers
    }

    public class PolicyHeaders : Dictionary<string, string>
    {
    }
}