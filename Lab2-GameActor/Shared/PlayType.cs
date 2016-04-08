using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum PlayType
    {
        Unknown = 0,
        None = 1,
        Out = 2,
        Strikeout = 3,
        StolenBase = 4,
        DefensiveIndifference = 5,
        CaughtStealing = 6,
        PickoffError = 7,
        Pickoff = 8,
        WildPitch = 9,
        PassedBall = 10,
        Balk = 11,
        OtherAdvance = 12,
        FoulError = 13,
        Walk = 14,
        IntentionalWalk = 15,
        HitByPitch = 16,
        Interference = 17,
        Error = 18,
        FieldersChoice = 19,
        Single = 20,
        Double = 21,
        Triple = 22,
        HomeRun = 23,
        MissingRlay = 24,
    }
}
