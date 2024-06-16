using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ChatHub : Hub
    {
        private static readonly ConnectionMapping<Guid> _connections = new ConnectionMapping<Guid>();

        public async Task AddToGroupAsync(Guid userId)
        {
            _connections.Add(userId, Context.ConnectionId);
        }

        public void RemoveFromGroupAsync(Guid userId)
        {
            _connections.Remove(userId, Context.ConnectionId);
        }

        public void RemoveAllFromGroup(Guid userId)
        {
            _connections.RemoveAll(userId);
        }

        public async Task SendMessage(Guid senderId, Guid receiverId, string content)
        {
            var groupName = $"{senderId}_{receiverId}";

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var connections = _connections.GetConnections(receiverId);
            foreach (var connectionId in connections)
            {
                await Groups.AddToGroupAsync(connectionId, groupName);
            }

            await Clients.Group(groupName).SendAsync("ReceivedMessage", senderId, receiverId, content);
        }
    }
}
