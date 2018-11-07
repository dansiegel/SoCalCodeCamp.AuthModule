using Prism.Logging.Syslog;

namespace SoCalCodeCamp.AuthDemo.Sample.Helpers
{
    class SyslogLoggerOptions : ISyslogOptions
    {
        string ISyslogOptions.HostNameOrIp => "192.168.1.10";
        int? ISyslogOptions.Port => 514;
        string ISyslogOptions.AppNameOrTag => "AuthDemo";
    }
}
