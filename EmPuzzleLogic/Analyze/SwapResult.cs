using System.Linq;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Analyze
{
    public class SwapResult
    {
        public int X { get; set; }

        public int Y { get; set; }

        public SwapType Direction { get; set; }

        public CollapseResult Result { get; set; }

        public int Weight { get; set; }

        public bool WeakShot { get; set; }

        public override string ToString()
        {
            var weakshot = WeakShot ? "!Weak!" : "......";
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
            return
                $"{X}:{Y} - {direction} - {weakshot} - {Result.SelectMany(r => r.Value).Sum(r => r.Value)} - {Result.Select(r => r.Value.Sum(tr => tr.Value).ToString()).Aggregate((o, n) => o.ToString() + "|" + n.ToString())}";
        }
    }
}