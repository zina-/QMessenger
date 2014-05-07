using Microsoft.AspNet.SignalR;
using QMessenger.Repository;
using System.Threading.Tasks;

namespace QMessenger.Hubs
{
    [Authorize]
    public class LobbyHub : Hub
    {
        public override Task OnConnected()
        {
            string connectionId = Context.ConnectionId;
            string userId = Context.User.Identity.Name;

            LocalCacheRepository.Instance.TryRepositoryAdd(userId, connectionId);

            Clients.Others.notify(connectionId, userId);
            Clients.Caller.notify(connectionId, userId);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string connectionId = Context.ConnectionId;
            string userId = Context.User.Identity.Name;

            LocalCacheRepository.Instance.TryRepositoryRemoveConnection(userId, connectionId);

            return base.OnDisconnected();
        }
    }
}