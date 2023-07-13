using HPASS.Crosscutting.Abstraction;

namespace HPASS.Crosscutting.Implementation
{
    public class UserSessionContext : IUserSessionContext
    {
        public Guid UserId { get; set; }
        public Guid HPID { get; set; }
        public string HPName { get; set; }
        public IEnumerable<string> Rights { get; set; }
        public DateTime ContextIssuedUtcTime { get; }

        public UserSessionContext()
        {
            this.Rights = new List<string>();
            this.ContextIssuedUtcTime = DateTime.UtcNow;
        }
    }
}