using System.Collections.Generic;
using System.Linq;

namespace EmPuzzleLogic.Analyze
{
    public class TitanSwapComparer : IComparer<SwapResult>
    {
        public int Compare(SwapResult x, SwapResult y)
        {
            if (x == null && y != null)
                return -1;
            if (y == null && x != null)
                return 1;
            if (x == null)
                return 0;
            if (x.WeakShot && !y.WeakShot)
                return 1;
            if (!x.WeakShot && y.WeakShot)
                return -1;

            if (x.NextTurnPropositions.Any(s => s.WeakShot) && !y.NextTurnPropositions.Any(s => s.WeakShot))
                return 1;
            if (y.NextTurnPropositions.Any(s => s.WeakShot) && !x.NextTurnPropositions.Any(s => s.WeakShot))
                return -1;

            return x.Weight - y.Weight;
        }
    }
}