namespace ZetaMinusOne.PolicyGuard.ASPNETCore
{
    /// <summary>
    /// Class for storing the ApiKey of your 
    /// Registered Domain in <seealso href="https://www.policyguard.io/"/>
    /// </summary>
    public class PolicyGuardCredential
    {
        /// <summary>
        /// Assigned Api Key for your Domain.
        /// Visit <seealso href="https://www.policyguard.io/"/> 
        /// to optain your Api Key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}