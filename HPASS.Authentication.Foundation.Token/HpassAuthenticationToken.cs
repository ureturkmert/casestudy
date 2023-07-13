namespace HPASS.Authentication.Foundation.Token
{
    public class HpassAuthenticationToken
    {
        public string Token { get; set; }

        public DateTime TokenIssuedAt { get; set; }

        public DateTime TokenValidAfter { get; set; }

        public DateTime TokenValidUntil { get; set; }

        public double TokenValidPeriodAsMinute { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}