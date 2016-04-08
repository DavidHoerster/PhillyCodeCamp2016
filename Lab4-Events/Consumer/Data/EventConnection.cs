using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Consumer.Data
{
    public class EventConnection
    {
        public static IEventStoreConnection Connection;
        
        public EventConnection()
        {
            EventConnection.Connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            EventConnection.Connection.ConnectAsync().Wait();
        }
    }
}
