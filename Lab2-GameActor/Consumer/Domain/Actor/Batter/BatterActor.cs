using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Data;
using Consumer.Domain.Messages;
using Consumer.Entity;
using MongoDB.Driver;

namespace Consumer.Domain.Actor
{
    public class BatterActor : ReceiveActor
    {
        public static Props Create(String id)
        {
            return Props.Create<BatterActor>(() => new BatterActor(id));
        }

        private readonly String _id;
        private Int32 _totalBases = 0;
        private Int32 _atBats = 0;
        private Int32 _hits = 0;
        private Int32 _walks = 0;
        private Int32 _hitByPitch = 0;
        private Int32 _sacrificeFlies = 0;
        private Int32 _rbi = 0;

        IMongoCollection<Batter> _coll = MongoConnection.Database.GetCollection<Batter>("batter");

        public BatterActor(String id)
        {
            _id = id;

            Receive<HitterWasAtBat>(msg =>
            {
                _totalBases += msg.HitValue;
                _atBats += msg.IsAtBat ? 1 : 0;
                _rbi += msg.RbiOnPlay;
                _hits += msg.HitValue > 0 ? 1 : 0;
                if (msg.PlayType == Shared.PlayType.Walk)
                {
                    _walks += 1;
                }
                else if (msg.PlayType == Shared.PlayType.HitByPitch)
                {
                    _hitByPitch += 1;
                }
                else if (msg.IsSacrificeFly)
                {
                    _sacrificeFlies += 1;
                }

                //Console.WriteLine($"{_id} has {Average()} BA, {OnBase()} OBP, and {Slugging()} SLG");

                _coll.ReplaceOneAsync<Batter>(b => b.Id.Equals(_id),
                    new Batter
                    {
                        AtBats = _atBats,
                        Average = Average(),
                        Hits = _hits,
                        Id = _id,
                        Name = "",
                        OnBase = OnBase(),
                        SacrificeFlies = _sacrificeFlies,
                        Slugging = Slugging(),
                        Walks = _walks,
                        HitByPitch = _hitByPitch,
                        TotalBases = _totalBases
                    }, new UpdateOptions { IsUpsert = true })
                    .PipeTo(Self);
            });
        }

        private Double Average()
        {
            if (_atBats == 0)
            {
                return 0.0;
            }
            return ((Double)_hits / (Double)_atBats);
        }

        private Double Slugging()
        {
            if (_atBats == 0 )
            {
                return 0.0;
            }
            return ((Double)(_totalBases)) / (Double)_atBats;
        }

        private Double OnBase()
        {
            return ((Double)(_hits + _walks + _hitByPitch) / ((Double)(_atBats + _walks + _hitByPitch + _sacrificeFlies)));
        }
    }
}
