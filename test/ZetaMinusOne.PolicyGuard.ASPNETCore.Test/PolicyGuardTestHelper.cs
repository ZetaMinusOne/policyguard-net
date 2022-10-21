using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore.Test
{
    public class PolicyGuardTestHelper
    {
        public static Mock<HttpMessageHandler> FakeMessageHandler(
            HttpResponseMessage mockedResponse)
        {
            Mock<HttpMessageHandler> mockHandler = new();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockedResponse);

            return mockHandler;
        }

        public static HttpResponseMessage FakeHttpResponse(
            PolicyHeaders headers, HttpStatusCode statusCode)
        {
            string json = JsonConvert.SerializeObject(headers);
            HttpResponseMessage httpResponse = new();
            httpResponse.StatusCode = statusCode;
            httpResponse.Content = new StringContent(
                json, Encoding.UTF8, "application/json");
            return httpResponse;
        }

    }
}