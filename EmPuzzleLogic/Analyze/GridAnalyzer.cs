﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Emgu.CV.XImgproc;
using EmPuzzleLogic.Behaviour;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Analyze
{
    public class GridAnalyzer
    {
        public static List<SwapResult> GetPossibleSwaps(Grid grid)
        {
            List<SwapResult> results = new List<SwapResult>();
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    var testGrid = grid.Clone();
                    var pointed = ActionFactory.GetBehaviour(testGrid[i, j]).Action(SwapType.Point);
                    var collapsing = testGrid.GetCollapsingCells().Union(pointed).ToList();
                    if (collapsing.Count() > 0)
                    {
                        SwapResult swap = new SwapResult() {X = i, Y = j, Direction = SwapType.Point};
                        swap.Result = new CollapseResult(grid.Width);
                        while (collapsing.Any())
                        {
                            var newCollapsing = new List<CellItem>(collapsing);
                            foreach (var cellItem in collapsing)
                            {
                                newCollapsing.AddRange(ActionFactory.GetBehaviour(cellItem)
                                    .Action(SwapType.Kill, newCollapsing));
                            }

                            swap.Result.Append(testGrid.Collapse(newCollapsing));
                            collapsing = testGrid.GetCollapsingCells().Distinct().ToList();
                        }

                        results.Add(swap);
                    }
                }
            }

            for (int i = 0; i < grid.Width - 1; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    var testGrid = grid.Clone();
                    testGrid = grid.Clone();
                    ActionFactory.GetBehaviour(testGrid[i, j]).Action(SwapType.Right);
                    var collapsing = testGrid.GetCollapsingCells().ToList();
                    if (collapsing.Count() > 0)
                    {
                        SwapResult swap = new SwapResult() {X = i, Y = j, Direction = SwapType.Right};
                        swap.Result = new CollapseResult(grid.Width);
                        while (collapsing.Any())
                        {
                            var newCollapsing = new List<CellItem>(collapsing);
                            foreach (var cellItem in collapsing)
                            {
                                newCollapsing.AddRange(ActionFactory.GetBehaviour(cellItem)
                                    .Action(SwapType.Kill, newCollapsing));
                            }

                            swap.Result.Append(testGrid.Collapse(newCollapsing));
                            collapsing = testGrid.GetCollapsingCells().Distinct().ToList();
                        }

                        results.Add(swap);
                    }
                }
            }

            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height - 1; j++)
                {
                    var testGrid = grid.Clone();

                    testGrid = grid.Clone();
                    ActionFactory.GetBehaviour(testGrid[i, j]).Action(SwapType.Down);
                    var collapsing = testGrid.GetCollapsingCells().Distinct().ToList();
                    if (collapsing.Count() > 0)
                    {
                        SwapResult swap = new SwapResult() {X = i, Y = j, Direction = SwapType.Down};
                        swap.Result = new CollapseResult(grid.Width);
                        while (collapsing.Any())
                        {
                            var newCollapsing = new List<CellItem>(collapsing);
                            foreach (var cellItem in collapsing)
                            {
                                newCollapsing.AddRange(ActionFactory.GetBehaviour(cellItem)
                                    .Action(SwapType.Kill, newCollapsing));
                            }

                            swap.Result.Append(testGrid.Collapse(newCollapsing));
                            collapsing = testGrid.GetCollapsingCells().Distinct().ToList();
                        }

                        results.Add(swap);
                    }
                }
            }

            
            foreach (var swapResult in results)
            {
                int weight = 0;
                foreach (var pair in swapResult.Result)
                {
                    foreach (var i in pair.Value)
                    {
                        weight += GetCellValue(i.Key, grid._enemies[pair.Key]);
                    }
                }

                if (grid.WeakSlot >= 0)
                {
                    if (swapResult.Result[grid.WeakSlot].Sum(r => r.Value) >= 3)
                        weight *= 5;
                }

                swapResult.Weight = weight;
            }

            return results.OrderByDescending(r => r.Weight).ToList();
        }

        public static int GetCellValue(CellColor cell, CellColor enemy)
        {
            switch (enemy)
            {
                case CellColor.Blue:
                    return cell == CellColor.Green ? 20 : cell == CellColor.Red ? 5 : 10;
                case CellColor.Green:
                    return cell == CellColor.Red ? 20 : cell == CellColor.Blue ? 5 : 10;
                case CellColor.Red:
                    return cell == CellColor.Blue ? 20 : cell == CellColor.Green ? 5 : 10;
                case CellColor.Light:
                    return cell == CellColor.Dark ? 20 : cell == CellColor.Light ? 5 : 10;
                case CellColor.Dark:
                    return cell == CellColor.Dark ? 20 : cell == CellColor.Dark ? 5 : 10;
                case CellColor.None:
                    return 3;
            }

            return 0;
        }

        public static List<CellItem> GetGeneratedCells(List<CellItem> killed)
        {
            List<CellItem> result = new List<CellItem>();
            foreach (var color in killed.GroupBy(c => c.Tag))
            {
                List<(int min, int max, int coord, bool vert)> groups = new List<(int, int, int, bool)>();
                
                foreach (var horzLine in color.GroupBy(c => c.Position.Y))
                {
                    var orderedLine = horzLine.OrderBy(c => c.Position.X).ToArray();
                    int count = 1;
                    for (int i = 0; i + count < orderedLine.Length - 1; i++)
                    {
                        count = 1;
                        while (i + count < orderedLine.Length && orderedLine[i].Position.X == orderedLine[i + count].Position.X - count)
                        {
                            count++;
                        }

                        i += count - 1;
                        if (count > 2)
                        {
                            groups.Add((i, i + count - 1, horzLine.Key, false));
                        }
                    }
                }
                foreach (var vertLine in color.GroupBy(c => c.Position.X))
                {
                    var orderedLine = vertLine.OrderBy(c => c.Position.Y).ToArray();
                    int count = 0;
                    for (int i = 0; i + count < orderedLine.Length - 1; i++)
                    {
                        count = 1;
                        while (i + count < orderedLine.Length && orderedLine[i].Position.Y == orderedLine[i + count].Position.Y - count)
                        {
                            count++;
                        }

                        i += count - 1;
                        if (count > 2)
                        {
                            groups.Add((i, i + count - 1, vertLine.Key, true));
                        }
                    }
                }

                if(groups.Count() == 0)
                    continue;

                List<(int, int, int, bool)> killedGroup = new List<(int, int, int, bool)>();
                var group = groups.Take(1).ToList();
                killedGroup.AddRange(group);
                groups.Remove(group.Single());
                while (groups.Count() > 0)
                {
                    foreach (var valueTuple in @group)
                    {
                        var inter = groups.Where(g =>
                            g.vert != valueTuple.vert && g.coord >= valueTuple.min && g.coord <= valueTuple.max).Distinct();
                        if (inter.Count() == 0)
                        {
                            var newItem = CheckKilledGroup(killedGroup, color.Key);
                            if (newItem != null)
                            {
                                result.Add(newItem);
                            }
                            killedGroup.Clear();
                            group = groups.Take(1).ToList();
                            killedGroup.AddRange(group);
                            groups.Remove(group.Single());
                        }
                        group.RemoveAll(r => inter.Any(i =>
                            i.min == r.min && i.max == r.max && i.coord == r.coord && i.vert == r.vert));
                        killedGroup.AddRange(inter);
                        group = inter.ToList();
                    }
                }
            }

            return result;
        }

        private static CellItem CheckKilledGroup(List<(int min, int max, int coord, bool vert)> killedGroup, CellColor tag)
        {
            List<CellPosition> items = new List<CellPosition>();
            killedGroup.ForEach(kg =>
            {
                for (int i = kg.min; i <= kg.max; i++)
                {
                    items.Add(kg.vert ? new CellPosition(kg.coord, i) : new CellPosition(i, kg.coord));
                }
            });
            var result = new CellItem();
            result.Position = new CellPosition((int)items.Average(i => i.X), (int)items.Average(i => i.Y));
            if (items.Count > 4)
            {
                result.Type = CellType.Crystal;
                return result;
            }

            if (items.Count > 3)
            {
                result.Type = CellType.Dragon;
                return result;
            }

            return null;
        }
    }
}