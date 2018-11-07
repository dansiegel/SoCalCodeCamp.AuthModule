# Implementations & Registrations

- You will need to implement IAADOptions in the Prism Application
- Each Platform will need to implement `IPlatformInitializer` with a registration for the UIParent

```json
{
  "LoggingServer": "192.168.1.10",
  "LoggingPort": 514
  "TenantName": "fabrikamb2c",
  "ClientId": "90c0fe63-bcf2-44d5-8fb7-b8bbc0b29dc6",
  "Scopes": "https://fabrikamb2c.onmicrosoft.com/helloapi/demo.read",
  "Policy": "b2c_1_susi"
}
```

NOTE: The following snippet assumes that you have installed the Mobile.BuildTools package

```cs
class SyslogOptions : ISyslogOptions
{
    string ISyslogOptions.HostNameOrIp => Secrets.LoggingServer;
    int? ISyslogOptions.Port => Secrets.LoggingPort;
    string ISyslogOptions.AppNameOrTag => "Awesome App";
}

class SampleAADOptions : IAADOptions
{
    string IAADOptions.ClientId => Secrets.ClientId;
    string IAADOptions.Policy => Secrets.Policy;
    string IAADOptions.Tenant => Secrets.TenantName;
    string[] IAADOptions.Scopes => Secrets.Scopes.Split(',');
}
```