using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Analyze
{
    public class SwapResult
    {
        private List<SwapResult> _nextTurnProposition;

        public int X { get; set; }

        public int Y { get; set; }

        public SwapType Direction { get; set; }

        public CollapseResult Result { get; set; }

        public int Weight { get; set; }

        public bool WeakShot { get; set; }

        public List<CellItem> Generated { get; set; } = new List<CellItem>();

        public Grid FinalGrid { get; set; }

        public bool CanPropose { get; set; } = true;

        public List<SwapResult> NextTurnPropositions
        {
            get
            {
                if (!CanPropose)
                    return null;
                if (_nextTurnProposition == null)
                {
                    _nextTurnProposition = GridAnalyzer.GetPossibleSwaps(FinalGrid);
                    _nextTurnProposition.ForEach(f => f.CanPropose = false);
                }

                return _nextTurnProposition;
            }
        }

        public override string ToString()
        {
            var weakshot = WeakShot ? "!Weak!" : ".miss.";
            string direction = "";
            switch (Direction)
            {
                case SwapType.Right: direction = ">";
                    break;
                case SwapType.Down: direction = "v";
                    break;
                case SwapType.Point: direction = "x";
                    break;
            }

            var propose = "";
            if (NextTurnPropositions != null && NextTurnPropositions.Count > 0)
            {
                propose = (NextTurnPropositions.First().WeakShot ? $"!Weak-Next!" : ".miss-next.") +
                          $"{NextTurnPropositions.First().Result.Total}";
            }
            return
                $"{X}:{Y} - {direction} - {weakshot} - {Result.SelectMany(r => r.Value).Sum(r => r.Value)} - {Result.Select(r => r.Value.Sum(tr => tr.Value).ToString()).Aggregate((o, n) => o.ToString() + "|" + n.ToString())} - Proposed:{propose}";
        }
    }
}