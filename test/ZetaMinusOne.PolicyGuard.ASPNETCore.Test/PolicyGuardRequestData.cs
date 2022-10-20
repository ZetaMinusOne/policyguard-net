using System.Net;

namespace ZetaMinusOne.PolicyGuard.ASPNETCore.Test
{
    public class PolicyGuardRequestData 
    {
        public static IEnumerable<object[]> GetRequest()
        {
            return new List<object[]>
            {
                // data with invalid apikey
                new object []
                {
                    new PolicyHeaders(),
                    "",
                    HttpStatusCode.InternalServerError
                },
                new object []
                {
                    new PolicyHeaders(),
                    "123432",
                    HttpStatusCode.InternalServerError
                },
                new object []
                {
                    new PolicyHeaders(),
                    "2131230912830918318",
                    HttpStatusCode.InternalServerError
                },

                // data with "valid" apikey
                new object []
                {
                    new PolicyHeaders {
                        {"header1", "value1" },
                    },
                    "1234567890",
                    HttpStatusCode.OK
                },
                new object []
                {
                    new PolicyHeaders {
                        {"header1", "value1" },
                        {"header2", "value2" },
                    },
                    "12345690",
                    HttpStatusCode.OK
                },
                new object []
                {
                    new PolicyHeaders {
                        {"header1", "value1" },
                        {"header2", "value2" },
                        {"header3", "value3" },
                    },
                    "14560",
                    HttpStatusCode.OK
                },
            };
        }
    }
}