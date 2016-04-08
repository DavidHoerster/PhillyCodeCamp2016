using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Consumer.Data;
using Consumer.Domain.Messages;
using MongoDB.Driver;
using Shared;

namespace Consumer.Domain.Actor
{
    public class PitcherActor : ReceiveActor
    {
        public static Props Create(String id)
        {
            return Props.Create<PitcherActor>(() => new PitcherActor(id));
        }

        private readonly String _id;
        private readonly String _name;
        private Int32 _totalBases = 0;
        private Int32 _atBats = 0;
        private Int32 _hits = 0;
        private Int32 _walks = 0;
        private Int32 _hitByPitch = 0;
        private Int32 _sacrificeFlies = 0;

        IMongoCollection<Pitcher> _coll = MongoConnection.Database.GetCollection<Pitcher>("pitcher");

        public PitcherActor(String id)
        {
            _id = id;

            Receive<PitcherFacedBatter>(msg =>
            {
                _totalBases += msg.HitValue;
                _atBats += msg.IsAtBat ? 1 : 0;
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

                _coll.ReplaceOneAsync<Pitcher>(b => b.Id.Equals(_id),
                                            new Pitcher
                                            {
                                                AtBats = _atBats,
                                                OppAvg = Average(),
                                                Hits = _hits,
                                                Id = _id,
                                                Name = "",
                                                OppObp = OnBase(),
                                                SacrificeFlies = _sacrificeFlies,
                                                OppSlugging = Slugging(),
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
            if (_atBats == 0)
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
