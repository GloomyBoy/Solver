using System;
using System.Collections.Generic;
using EmPuzzleLogic.Analyze;
using EmPuzzleLogic.Entity;
using GridTest.Generator;
using NUnit.Framework;

namespace GridTest
{
    [TestFixture]
    public class Playground
    {
        [Test]
        public void Test_1()
        {
            var gridStr =
@"None|Blue|Blue|Blue|Blue|Blue|None
4
R:R|R:B|R:R|R:B|R:R|R:B|R:R
R:R|R:B|R:R|R:L|R:L|R:L|R:R
R:G|R:D|R:G|R:D|R:G|R:L|R:G
R:L|R:L|R:L|R:L|R:G|R:L|R:G
R:R|R:L|R:L|R:L|R:R|R:B|R:R";
            var grid = GridGenerator.GetGrid(gridStr);
           // var swaps = GridAnalyzer.GetPossibleSwaps(grid);
            var killed = GridAnalyzer.GetGeneratedCells(new List<CellItem>()
            {
                grid[0, 3],
                grid[1, 3],
                grid[1, 4],
                grid[2, 3],
                grid[2, 4],
                grid[3, 1],
                grid[3, 3],
                grid[3, 4],
                grid[3, 1],
                grid[4, 1],
                grid[5, 1],
                grid[5, 2],
                grid[5, 3]
            });
        }
    }
}
