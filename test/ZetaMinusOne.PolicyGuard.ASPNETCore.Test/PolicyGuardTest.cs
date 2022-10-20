using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Text;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore.Test
{
    public class PolicyGuardTest
    {
        [Theory]
        [MemberData(
            nameof(PolicyGuardRequestData.GetRequest), 
            MemberType = typeof(PolicyGuardRequestData))]
        public async Task RequestPolicyHeadersToPolicyGuardAPI(
            PolicyHeaders expectedHeaders, string apiKey, HttpStatusCode statusCode)
        {
            // Arrange
            var httpResponse = PolicyGuardTestHelper.FakeHttpResponse(expectedHeaders, statusCode);
            Mock<HttpMessageHandler> mockHandler = PolicyGuardTestHelper.FakeMessageHandler(httpResponse); 
            HttpClient httpClient = new(mockHandler.Object);
            var sut = new PolicyGuard(httpClient);

            // Act
            PolicyHeaders actualHeaders = await sut.GetPolicyHeadersAsync(apiKey);

            // Assert
            Assert.Equal(expectedHeaders, actualHeaders);
        }
    }
}