using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Shared.Events;

namespace EventStoreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var conn = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
            {
                conn.ConnectAsync().Wait();


                while (true)
                {

                    Console.WriteLine("Enter player ID:");
                    var playerId = Console.ReadLine();

                    if (playerId == "EXIT")
                    {
                        break;
                    }

                    var slice = conn.ReadStreamEventsForwardAsync(playerId,
                                                                    StreamPosition.Start,
                                                                    1000,
                                                                    false).Result;

                    var events = slice.Events.Select(e => JsonConvert.DeserializeObject<HitterHadPlateAppearance>(Encoding.UTF8.GetString(e.Event.Data)));

                    var batter = new Batter(playerId);
                    batter.LoadFromHistory(events);
                    Console.WriteLine($"{batter.Id} hit {batter.Average()} with a {batter.Slugging()} slugging along with {batter.HomeRuns} homers and {batter.Rbis} rbis");

                    batter.ClearEvents();
                    for (int balls = 0; balls < 4; balls++)
                    {
                        for (int strikes = 0; strikes < 3; strikes++)
                        {
                            batter.ForPitchCount(balls, strikes, events);
                            Console.WriteLine($"For a {balls}-{strikes} count, McCutchen batted {batter.Average()} and hit {batter.HomeRuns} homers");
                            batter = new Batter(playerId);
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
