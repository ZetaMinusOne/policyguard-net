using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore.Test
{
    public class PolicyGuardTest
    {
        [Theory]
        [MemberData(
            nameof(PolicyGuardRequestData.GetRequest),
            MemberType = typeof(PolicyGuardRequestData))]
        public async Task Request_policy_headers_from_Policy_Guard_API(
            PolicyHeaders expectedHeaders, string apiKey, HttpStatusCode statusCode)
        {
            // Arrange
            var httpResponse = 
                PolicyGuardTestHelper.FakeHttpResponse(expectedHeaders, statusCode);
            Mock<HttpMessageHandler> mockHandler = 
                PolicyGuardTestHelper.FakeMessageHandler( httpResponse);
            HttpClient httpClient = new(mockHandler.Object);
            var sut = new PolicyGuard(httpClient, apiKey);

            // Act
            PolicyHeaders actualHeaders = 
                await sut.GetPolicyHeadersAsync();

            // Assert
            Assert.Equal(expectedHeaders, actualHeaders);
        }

        [Theory]
        [MemberData(
            nameof(PolicyGuardRequestData.GetRequest),
            MemberType = typeof(PolicyGuardRequestData))]
        public async Task Update_the_policy_headers(
            PolicyHeaders expectedHeaders, string apiKey, HttpStatusCode statusCode)
        {
            // Arrange
            var httpResponse =
                PolicyGuardTestHelper.FakeHttpResponse(expectedHeaders, statusCode);
            Mock<HttpMessageHandler> mockHandler =
                PolicyGuardTestHelper.FakeMessageHandler(httpResponse);
            HttpClient httpClient = new(mockHandler.Object);
            var sut = new PolicyGuard(httpClient, apiKey);

            // Act
            var context =
                await sut.SetPolicyGuardHeaders(new DefaultHttpContext());
            var actualHeaders = context.Response.Headers.ToDictionary(
                x => x.Key, x=> x.Value.ToString());

            // Assert
            Assert.Equal(expectedHeaders, actualHeaders);
        }
    }
}