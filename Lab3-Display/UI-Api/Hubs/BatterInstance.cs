using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Shared;

namespace UI_Api.Hubs
{
    public class BatterInstance
    {
        // Singleton instance
        private readonly static Lazy<BatterInstance> _instance = new Lazy<BatterInstance>(() => new BatterInstance(GlobalHost.ConnectionManager.GetHubContext<BatterHub>().Clients));
        private BatterInstance(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }
        public static BatterInstance Instance { get { return _instance.Value; } }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        public void UpdateBatterStat(Batter batter)
        {
            Clients.Group(batter.PlayerId).updateStats(batter);
        }
    }
}