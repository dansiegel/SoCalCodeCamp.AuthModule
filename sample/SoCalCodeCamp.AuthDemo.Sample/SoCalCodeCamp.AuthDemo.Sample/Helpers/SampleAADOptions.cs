using SoCalCodeCamp.AuthDemo.Services;

namespace SoCalCodeCamp.AuthDemo.Sample.Helpers
{
    class SampleAADOptions : IAADOptions
    {
        string IAADOptions.ClientId => Secrets.ClientId;
        string IAADOptions.Policy => Secrets.Policy;
        string IAADOptions.Tenant => Secrets.TenantName;
        string[] IAADOptions.Scopes => Secrets.Scopes.Split(',');
    }
}
