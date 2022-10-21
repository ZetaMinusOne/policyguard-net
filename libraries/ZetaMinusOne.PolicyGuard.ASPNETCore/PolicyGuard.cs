using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    public class PolicyGuard
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        private const string uri = "https://www.policyguard.io/api/csp/";
        private const double expiryTime = 10;

        private PolicyHeaders _headers;
        private DateTime lastTime;
        private bool fetching;

        public PolicyGuard(HttpClient httpClient, string apiKey)
        {
            _headers = new();
            _httpClient = httpClient;
            _apiKey = apiKey;
        }


        // Get Policy Headers
        public async Task<PolicyHeaders> GetPolicyHeadersAsync()
        {
            string apiUrl = uri + _apiKey;
            var res = await _httpClient.GetAsync(apiUrl);

            if (res.IsSuccessStatusCode)
            {
                string data = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PolicyHeaders>(data)
                    ?? _headers;
            }

            Console.Error.WriteLine($"Failed to get policy header from ${apiUrl}");
            return _headers;
        }

        public DateTime GetPolicyExpiration() => lastTime.AddSeconds(expiryTime);

        public bool IsPolicyExpired() => DateTime.Now > GetPolicyExpiration();

        public async Task UpdatePolicyHeadersCache()
        {
            if (fetching) return;
            fetching = true;
            _headers = await GetPolicyHeadersAsync();
            lastTime = DateTime.Now;
            fetching = false;
        }

        public async Task<HttpContext> SetPolicyGuardHeaders(HttpContext context)
        {
            if (IsPolicyExpired()) await UpdatePolicyHeadersCache();

            foreach (var header in _headers)
                context.Response.Headers[header.Key] = header.Value;

            return context;
        }
    }
}