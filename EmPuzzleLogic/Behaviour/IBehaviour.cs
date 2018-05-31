using System;
using System.Collections.Generic;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Behaviour
{
    public interface IBehaviour
    {
        List<CellItem> Action(SwapType action, List<CellItem> processed = null);
    }

    public abstract class ABehaviour : IBehaviour
    {
        protected CellItem _item;

        protected ABehaviour(CellItem item)
        {
            _item = item;
        }

        public abstract List<CellItem> Action(SwapType action, List<CellItem> processed = null);

        protected void Swap(CellItem item1, CellItem item2)
        {
            if(item1.Parent != item2.Parent)
                throw new ArgumentException("Cross grid swap!");
            var position1 = item1.Position;
            var position2 = item2.Position;
            item1.Parent[position1] = item2;
            item2.Parent[position2] = item1;
        }
    }
}