namespace SoCalCodeCamp.AuthDemo.Services
{
    public interface IAADOptions
    {
        string ClientId { get; }
        string Policy { get; }
        string Tenant { get; }
        string[] Scopes { get; }
    }
}