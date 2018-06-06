using System;
using System.CodeDom.Compiler;
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
                case CellType.Crystal:
                    type = "C";
                    break;
                case CellType.Dragon:
                    type = "D";
                    break;
                case CellType.Regular:
                    type = "R";
                    break;
            }

            var tag = "-";
            tag = Tag.ToString().Substring(0, 1);
            return $"{type}:{tag}";
            return $"{Position}:{type}:{tag}";
        }

        public static CellItem Parse(string value)
        {
            if (value.Length != 3)
                throw new ArgumentException($"Can't parse CellItem from {value}");
            CellType type = CellType.None;
            switch (value[0])
            {
                case 'C':
                    type = CellType.Crystal;
                    break;
                case 'D':
                    type = CellType.Dragon;
                    break;
                case 'R':
                    type = CellType.Regular;
                    break;
            }
            CellColor color = CellColor.None; 
            switch (value[2])
            {
                case 'B':
                    color = CellColor.Blue;
                    break;
                case 'R':
                    color = CellColor.Red;
                    break;
                case 'G':
                    color = CellColor.Green;
                    break;
                case 'D':
                    color = CellColor.Dark;
                    break;
                case 'L':
                    color = CellColor.Light;
                    break;
            }
            return new CellItem(type, color);
        }

        public static string EmptyString()
        {
            return "---";
            return "[---]:[-]:[-]";
        }
    }
}