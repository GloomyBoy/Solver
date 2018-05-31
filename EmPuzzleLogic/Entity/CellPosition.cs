namespace EmPuzzleLogic.Entity
{
    public struct CellPosition
    {
        public int X { get; }

        public int Y { get; }

        public CellPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CellPosition))
                return false;
            var objI = (CellPosition)obj;
            return objI.X == X && objI.Y == Y;
        }

        public override int GetHashCode()
        {
            return X * 100 + Y;
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }
}