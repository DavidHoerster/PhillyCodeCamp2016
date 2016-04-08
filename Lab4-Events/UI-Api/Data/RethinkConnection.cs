using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

namespace UI_Api.Data
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