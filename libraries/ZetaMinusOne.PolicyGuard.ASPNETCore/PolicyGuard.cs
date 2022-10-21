using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    public class PolicyGuard
    {
        private PolicyHeaders _headers;
        private readonly HttpClient _httpClient;
        private const string uri = "https://www.policyguard.io/api/csp/";
        private const double expiryTime = 10; 
        private DateTime lastTime;
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

            Console.Error.WriteLine($"Failed to get policy header from ${apiUrl}");
            return _headers;
        }

        #region Policy Expiration
        public DateTime GetPolicyExpiration() => lastTime.AddSeconds(expiryTime);

        public bool IsPolicyExpired() => DateTime.Now > GetPolicyExpiration();
        #endregion

        // Update Policy Headers
        public async void UpdatePolicyHeadersCache(string apiKey)
        {
            // only one fetch at a time
            if (fetching) return; 

            // set fetching bit to prevent someone else from fetching
            fetching = true; 
            // get the headers and update the cache
            _headers = await GetPolicyHeadersAsync(apiKey);
            // update our cache expiration
            lastTime = DateTime.Now;
            // allow others to update
            fetching = false;
        }



        // Set Policy Headers

        // With Policy Headers
    }
}