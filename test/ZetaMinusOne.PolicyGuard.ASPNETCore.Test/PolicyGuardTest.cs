using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Text;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore.Test
{
    public class PolicyGuardTest
    {
        [Fact]
        public async Task ReturnEmptyPolicyHeaderWhenInvalidApiKeyIsProvidedAsync()
        {
            // Arrange
            var expectedHeaders = new PolicyHeaders();
            var httpResponse = PolicyGuardTestHelper.FakeHttpResponse(
                expectedHeaders, System.Net.HttpStatusCode.InternalServerError);
            Mock<HttpMessageHandler> mockHandler = PolicyGuardTestHelper.FakeMessageHandler(httpResponse); 
            HttpClient httpClient = new(mockHandler.Object);
            var sut = new PolicyGuard(httpClient);

            // Act
            PolicyHeaders actualHeaders = await sut.GetPolicyHeadersAsync(string.Empty);

            // Assert
            Assert.Equal(expectedHeaders, actualHeaders);
        }
    }
}