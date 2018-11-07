using Microsoft.Identity.Client;
using Prism.Events;

namespace SoCalCodeCamp.AuthDemo.Events
{
    public class UserAuthenticatedEvent : PubSubEvent<AuthenticationResult>
    {
    }
}
