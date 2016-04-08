using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStoreTest;
using Shared.Events;

namespace EventStoreSample
{
    public class Batter : AggregateRoot
    {
        private String _id;
        private Int32 _hits, _rbis, _hitValue, _homeRuns, _walks, _atBats;

        public Batter(String id)
        {
            _hits = 0;
            _rbis = 0;
            _hitValue = 0;
            _homeRuns = 0;
            _walks = 0;
            _atBats = 0;

            _id = id;
        }

        public void ForPitchCount(Int32 balls, Int32 strikes, IEnumerable<HitterHadPlateAppearance> events)
        {
            var filteredEvents = events.Where(e => e.Balls == balls && e.Strikes == strikes);
            LoadFromHistory(filteredEvents);
        }

        public Double Average()
        {
            if (_atBats == 0)
            {
                return 0;
            }
            return ((Double)_hits / (Double)_atBats);
        }

        public Double Slugging()
        {
            if (_atBats == 0)
            {
                return 0;
            }
            return ((Double)_hitValue / (Double)_atBats);
        }

        public Int32 HomeRuns { get { return _homeRuns; } }
        public Int32 Walks { get { return _walks; } }
        public Int32 Rbis { get { return _rbis; } }

        private void Apply(HitterHadPlateAppearance e)
        {
            _rbis += e.RbiOnPlay;
            _hitValue += e.HitValue;
            _hits += e.HitValue > 0 ? 1 : 0;
            _atBats += e.IsAtBat ? 1 : 0;
            _walks += e.PlayType == Shared.PlayType.Walk ? 1 : 0;
            _homeRuns += e.PlayType == Shared.PlayType.HomeRun ? 1 : 0;
        }
    }
}
