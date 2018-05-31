using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using EmPuzzleLogic.Behaviour;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Analyze
{
    public class GridAnalyzer
    {
        public static void GetPossibleSwaps(Grid grid)
        {
            for (int i = 0; i < grid.Width - 1; i++)
            {
                for (int j = 0; j < grid.Height - 1; j++)
                {
                    var testGrid = grid.Clone();
                    ActionFactory.GetBehaviour(testGrid[i, j]).Action(SwapType.Right);
                    var collapsing = testGrid.GetCollapsingCells().ToList();
                    SwapResult swap = new SwapResult
                    {
                        X = i,
                        Y = j,
                        Result = new CollapseResult(grid.Width)
                    };
                    while (collapsing.Any())
                    {
                        var newCollapsing = new List<CellItem>(collapsing);
                        foreach (var cellItem in collapsing)
                        {
                            newCollapsing.AddRange(ActionFactory.GetBehaviour(cellItem).Action(SwapType.Kill, newCollapsing));
                        }
                        swap.Result.Append(testGrid.Collapse(newCollapsing));
                        collapsing = testGrid.GetCollapsingCells().ToList();
                    }
                }
            }
        }

        public static void ApplySwap(CellItem item, SwapType swapType)
        {
            SwapResult swap = new SwapResult
            {
                X = item.Position.X,
                Y = item.Position.Y,
                Result = new CollapseResult(item.Parent.Width)
            };
            ActionFactory.GetBehaviour(item).Action(swapType);
        }

        public static void ApplyCollapseStep(Grid grid)
        {
            var collapsing = grid.GetCollapsingCells().ToList();
            var newCollapsing = new List<CellItem>(collapsing);
            foreach (var cellItem in collapsing)
            {
                newCollapsing.AddRange(ActionFactory.GetBehaviour(cellItem).Action(SwapType.Kill, newCollapsing));
            }
            grid.Collapse(newCollapsing);
        }

        public static (CellPosition, CellType) GetNewItems(List<CellItem> killed)
        {
            foreach (var colorGroup in killed.GroupBy(c => c.Tag))
            {
                List<(int xStart, int xEnd, int Y)> horzGroups = new List<(int xStart, int xEnd, int Y)>();
                List<CellItem> grouped = new List<CellItem>();
                var currentColor = colorGroup.ToList();
                while (currentColor.Count > 0)
                {
                    var cell = currentColor.First();
                    List<CellItem> currentGroup = new List<CellItem>();
                    currentGroup.Add(cell);
                    var leftCell = colorGroup.SingleOrDefault(c => c.Position.X == cell.Position.X - 1 && c.Position.Y == cell.Position.Y);
                    var rightCell = colorGroup.SingleOrDefault(c => c.Position.X == cell.Position.X + 1 && c.Position.Y == cell.Position.Y);
                    var min = cell.Position.X;
                    var max = cell.Position.X;
                    while (leftCell != null)
                    {
                        currentGroup.Add(leftCell);
                        min = leftCell.Position.X;
                        leftCell = colorGroup.SingleOrDefault(c => c.Position.X == leftCell.Position.X - 1 && c.Position.Y == leftCell.Position.Y);
                    }
                    while (rightCell != null)
                    {
                        currentGroup.Add(rightCell);
                        max = rightCell.Position.X;
                        rightCell = colorGroup.SingleOrDefault(c => c.Position.X == cell.Position.X + 1 && c.Position.Y == cell.Position.Y);
                    }
                    if (currentGroup.Count > 2)
                    {
                        horzGroups.Add((min, max, currentGroup.First().Position.Y));
                        currentColor = currentColor.Except(currentGroup).ToList();
                    }
                }
            }

            return null;
        }
    }
}