namespace HPASS.Crosscutting.Abstraction
{
    public interface IUserSessionContext
    {
        Guid UserId { get; set; }
        Guid HPID { get; set; }
        string HPName { get; set; }
        IEnumerable<string> Rights { get; set; }
        DateTime ContextIssuedUtcTime { get; }

    }
}