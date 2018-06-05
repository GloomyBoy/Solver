using System.Collections.Generic;
using System.Linq;

namespace EmPuzzleLogic.Analyze
{
    public interface ITargetStrategy
    {
        IOrderedEnumerable<SwapResult> SortSwaps(List<SwapResult> results);
    }
}