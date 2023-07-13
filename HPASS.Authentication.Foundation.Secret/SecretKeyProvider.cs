namespace HPASS.Authentication.Foundation.Secret
{
    public class SecretKeyProvider
    {
        private static readonly string AuthenticationSecretEnvironmentKey = "HPASS_AUTHENTICATION_SECRET_KEY";

        public static string GetAuthenticationSecretKeyFromEnvironmentVariables()
        {

            string secretKey = Environment.GetEnvironmentVariable(AuthenticationSecretEnvironmentKey, EnvironmentVariableTarget.Machine);

            if (string.IsNullOrEmpty(secretKey) || string.IsNullOrWhiteSpace(secretKey))
            {
                secretKey = Environment.GetEnvironmentVariable(AuthenticationSecretEnvironmentKey, EnvironmentVariableTarget.User);

                if (string.IsNullOrEmpty(secretKey) || string.IsNullOrWhiteSpace(secretKey))
                {
                    secretKey = Environment.GetEnvironmentVariable(AuthenticationSecretEnvironmentKey, EnvironmentVariableTarget.Process);
                }
            }



            if (string.IsNullOrEmpty(secretKey) || string.IsNullOrWhiteSpace(secretKey))
            {
                //throw new Exception("Secret Key Cannot be Obtained From Environment Variables");
                secretKey = "7oGSLwvy3pf9ETgLL3q5jVqZSQRfxwQu";
            }

            return secretKey;
        }
    }
}