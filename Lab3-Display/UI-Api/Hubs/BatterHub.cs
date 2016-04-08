using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Shared;
using UI_Api.Utils;

namespace UI_Api.Hubs
{
    public class BatterHub : Hub
    {
        public Task Subscribe(String id)
        {
            List<String> groups;
            var success = SignalRConnectionToGroupsMap.TryRemoveConnection(Context.ConnectionId, out groups);

            if (groups!=null)
            {
                foreach (var group in groups)
                {
                    Groups.Remove(Context.ConnectionId, group);
                }
            }

            SignalRConnectionToGroupsMap.TryAddGroup(Context.ConnectionId, id);
            return Groups.Add(Context.ConnectionId, id);
        }
    }
}