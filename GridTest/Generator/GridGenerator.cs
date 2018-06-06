using System;
using System.Linq;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace GridTest.Generator
{
    public class GridGenerator
    {
        public static Grid GetGrid(string representation)
        {
            Grid resultGrid = null;
            var lines = representation.Split('\r');
            var enemies = lines[0].Split('|').Select(s => Enum.Parse(typeof(CellColor), s)).Cast<CellColor>();
            var weakSpot = int.Parse(lines[1]);
            var gridLines = lines.Skip(2).ToList();
            for (int i = 0; i < gridLines.Count(); i++)
            {
                var cellLine = gridLines[i].Split('|');
                for (int j = 0; j < cellLine.Count(); j++)
                {
                    if (resultGrid == null)
                    {
                        resultGrid = new Grid(cellLine.Count(), gridLines.Count()) {WeakSlot = weakSpot};
                        var ei = 0;
                        foreach (var e in enemies)
                        {
                            resultGrid._enemies[ei] = e;
                            ei++;
                        }
                    }
                    resultGrid[j, i] = CellItem.Parse(cellLine[j].Trim('\r', '\n', ' '));
                }
            }
            return resultGrid;
        }
    }
}