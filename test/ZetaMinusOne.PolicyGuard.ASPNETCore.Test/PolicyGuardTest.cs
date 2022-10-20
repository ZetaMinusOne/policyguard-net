namespace ZetaMinusOne.PolicyGuard.ASPNETCore.Test
{
    public class PolicyGuardTest
    {
        [Fact]
        public async Task ReturnEmptyPolicyHeaderWhenInvalidApiKeyIsProvidedAsync()
        {
            // Arrange
            var expectedHeaders = new PolicyHeaders();
            var sut = new PolicyGuard(new HttpClient());

            // Act
            PolicyHeaders actualHeaders = await sut.GetPolicyHeadersAsync(string.Empty);

            // Assert
            Assert.Equal(expectedHeaders, actualHeaders);
        }
    }
}