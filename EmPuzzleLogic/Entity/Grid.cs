using System;
using System.Collections.Generic;
using System.Linq;
using EmPuzzleLogic.Behaviour;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Entity
{
    public class Grid
    {
        private CellItem[,] _grid;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            _grid = new CellItem[width, height];
        }

        public CellItem this[int i, int j]
        {
            get { return _grid[i, j]; }
            set
            {
                if (value != null)
                {
                    value.Parent = this;
                    value.Position = new CellPosition(i, j);
                }
                _grid[i, j] = value;
            }
        }

        public CellItem this[CellPosition position]
        {
            get { return _grid[position.X, position.Y]; }
            set { this[position.X, position.Y] = value; }
        }

        public CollapseResult Collapse(List<CellItem> additional = null)
        {
            if (additional == null) 
                additional = new List<CellItem>();
            var collapsing = GetCollapsingCells();
            var inCollapse = new List<CellItem>();
            foreach (var cellItem in collapsing)
            {
                inCollapse.AddRange(ActionFactory.GetBehaviour(cellItem).Action(SwapType.Kill, inCollapse));
            }
            var result = inCollapse.Distinct().ToList();
            foreach (var cellItem in result.Union(additional))
            {
                this[cellItem.Position.X, cellItem.Position.Y] = null;
            }
            
            for (int i = 0; i < Width; i++)
            {
                for (int j = 1; j < Height; j++)
                {
                    var moveUp = j;
                    while (moveUp > 0 && this[i, moveUp] != null && this[i, moveUp - 1] == null)
                    {
                        this[i, moveUp - 1] = this[i, j];
                        this[i, moveUp] = null;
                        moveUp--;
                    }
                }
            }

            var ret = new CollapseResult(Width);
            ret.Append(result);
            return ret;
        }

        public IEnumerable<CellItem> GetCollapsingCells()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var collapsing = CollapsingCount(j, i, Direction.Right);
                    if(collapsing > 2)
                        for(int r = 0; r < collapsing; r++)
                            yield return this[j + r, i];
                    j += collapsing - 1;
                }
            }
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var collapsing = CollapsingCount(i, j, Direction.Down);
                    if (collapsing > 2)
                        for (int r = 0; r < collapsing; r++)
                            yield return this[i, j + r];
                    j += collapsing - 1;
                }
            }
            yield break;
        }

        private int CollapsingCount(int x, int y, Direction direction)
        {
            int count = 1;
            switch (direction)
            {
                case Direction.Right:
                    if (Width - x > 2)
                    {
                        var tag = this[x, y].Tag;
                        var index = x + count;
                        while (index < Width && tag == this[index, y].Tag)
                        {
                            count++;
                            index = x + count;
                        }
                    }
                    break;
                case Direction.Down:
                    if (Height - y > 2)
                    {
                        var tag = this[x, y].Tag;
                        var index = y + count;
                        while (index < Width && tag == this[x, index].Tag)
                        {
                            count++;
                            index = y + count;
                        }
                    }
                    break;
            }
            return count;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Height; i++)
            {
                string row = "|";
                for (int j = 0; j < Width; j++)
                {
                    row += this[j, i]?.ToString() ?? CellItem.EmptyString() + "|";
                }
                result += row + Environment.NewLine;
            }
            return result;
        }

        public Grid Clone()
        {
            var result = new Grid(Width, Height);
            foreach (var cellItem in this._grid)
            {
                result[cellItem.Position.X, cellItem.Position.Y] = new CellItem(cellItem.Type, cellItem.Tag);
            }
            return result;
        }
    }
}