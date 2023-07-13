namespace HPASS.Authentication.Foundation.Option
{
    public class TokenGenerationOptions
    {
        public DateTime TokenValidStartDate { get; set; }

        public DateTime TokenValidEndDate { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public Dictionary<string, string> ClaimsDictionary { get; set; }

        public TokenGenerationOptions()
        {
            this.TokenValidStartDate = DateTime.UtcNow;
            this.TokenValidEndDate = DateTime.UtcNow.AddHours(1);
            this.Issuer = null;
            this.Audience = null;
            this.ClaimsDictionary = new Dictionary<string, string>();
        }
    }
}