using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;
using RethinkDb.Driver;
using Shared;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = args.Length == 0 ? "PIT2015.csv" : args[0];
            var conn = RethinkDB.R
                       .Connection()
                       .Hostname("localhost")
                       .Port(RethinkDBConstants.DefaultPort)
                       .Timeout(30)
                       .Connect();
            var table = RethinkDB.R.Db("baseball").Table("plays");

            var count = 0;

            using (var stream = File.OpenRead($"C:\\Code\\GIT\\PhillyCodeCamp2016\\Data\\{fileName}"))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, true))
            {
                while (csv.ReadNextRecord() /*&& count < 1000*/)
                {

                    var keys = csv.Columns.ToList();
                    var rowDict = keys.ToDictionary(k => k.Name, k => csv[k.Name]);

                    PlayType playType;
                    var gameId = csv["GameId"];
                    var success = Enum.TryParse<PlayType>(csv["EventType"], out playType);
                    var id = Guid.NewGuid();
                    var homeTeam = gameId.Substring(0, 3);
                    var visitingTeam = csv["VisitingTeam"];
                    var sequence = csv["PitchSequence"];
                    var balls = Convert.ToInt32(csv["Balls"]);
                    var strikes = Convert.ToInt32(csv["Strikes"]);
                    var outs = Convert.ToInt32(csv["Outs"]);
                    var inning = Convert.ToInt32(csv["Inning"]);
                    var homeScore = Convert.ToInt32(csv["HomeScore"]);
                    var visitorScore = Convert.ToInt32(csv["VisitorScore"]);
                    var rbiOnPlay = Convert.ToInt32(csv["RbiOnPlay"]);
                    var hitValue = Convert.ToInt32(csv["HitValue"]);
                    var batter = csv["BatterId"];
                    var pitcher = csv["PitcherId"];
                    var isBatterEvent = csv["IsBatterEvent"].Equals("T", StringComparison.OrdinalIgnoreCase);
                    var isAtBat = csv["IsAtBat"].Equals("T", StringComparison.OrdinalIgnoreCase);
                    var isHomeAtBat = csv["IsHomeAtBat"].Equals("1");
                    var isEndGame = csv["IsEndGame"].Equals("T", StringComparison.OrdinalIgnoreCase);
                    var isSacFly = csv["IsSacFly"].Equals("T", StringComparison.OrdinalIgnoreCase);

                    var @event = new Play(gameId, playType, homeTeam, visitingTeam,
                        sequence, inning, balls, strikes, outs,
                        homeScore, visitorScore, rbiOnPlay, hitValue,
                        batter, pitcher, 
                        isBatterEvent, isAtBat, isHomeAtBat, isEndGame, isSacFly);

                    table.Insert(@event).Run(conn);

                    count += 1;
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
