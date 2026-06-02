using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Tracker.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
    }
}
