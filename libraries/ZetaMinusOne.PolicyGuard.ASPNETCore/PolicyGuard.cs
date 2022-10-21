using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
            string apiUrl = uri + apikey;
            var res = await _httpClient.GetAsync(apiUrl);

            if (res.IsSuccessStatusCode)
            {
                string data = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PolicyHeaders>(data) 
                    ?? _headers;
            }

            return _headers;
        }

        // Get Policy Expiration
        // Is Policy Expired
        // Update Policy Headers
        // Set Policy Headers
        // With Policy Headers
    }
}