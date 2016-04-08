using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

namespace Consumer.Data
{
    public class RethinkConnection
    {
        public static Connection Connection { get; private set; }
        public RethinkConnection()
        {
            RethinkConnection.Connection = RethinkDB.R
                                            .Connection()
                                            .Hostname("localhost")
                                            .Port(RethinkDBConstants.DefaultPort)
                                            .Timeout(30)
                                            .Connect();
        }
    }
}
