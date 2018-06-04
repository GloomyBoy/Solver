using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Entity
{
    public class CellItem
    {
        public Grid Parent { get; set; }
        public CellPosition Position { get; set; }
        public CellType Type { get; set; }
        public CellColor Tag { get; set; }
        public CellItem(CellType type = CellType.None, CellColor tag = 0)
        {
            Type = type;
            Tag = tag;
        }

        public override string ToString()
        {
            string type = "-";
            switch (Type)
            {
                case CellType.Crystal: type = "C";
                    break;
                case CellType.Dragon: type = "D";
                    break;
                case CellType.Regular: type = "R";
                    break;
            }

            var tag = "-";
            tag = Tag.ToString().Substring(0, 1);
            return $"{type}:{tag}";
            return $"{Position}:{type}:{tag}";
        }

        public static string EmptyString()
        {
            return "---";
            return "[---]:[-]:[-]";
        }
    }
}