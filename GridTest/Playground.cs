using System;
using EmPuzzleLogic.Analyze;
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
R:R|R:B|R:R|R:B|R:R|R:L|R:R
R:R|R:B|R:R|R:L|R:L|R:B|R:R
R:G|R:D|R:G|R:D|R:G|R:L|R:G
R:G|R:D|R:G|R:D|R:G|R:L|R:G
R:R|R:B|R:R|R:B|R:R|R:B|R:R";
            var grid = GridGenerator.GetGrid(gridStr);
            var swaps = GridAnalyzer.GetPossibleSwaps(grid);
        }
    }
}
