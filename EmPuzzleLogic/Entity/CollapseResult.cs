using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic.Entity
{
    public class CollapseResult : IEnumerable<KeyValuePair<int, Dictionary<CellColor, int>>>
    {
        private readonly Dictionary<int, Dictionary<CellColor, int>> _collapsedByColumns =
            new Dictionary<int, Dictionary<CellColor, int>>();

        public CollapseResult(int width)
        {
            foreach (var i in Enumerable.Range(0, width))
                _collapsedByColumns[i] = new Dictionary<CellColor, int>
                {
                    {CellColor.Blue, 0},
                    {CellColor.Red, 0},
                    {CellColor.Dark, 0},
                    {CellColor.Light, 0},
                    {CellColor.Green, 0}
                };
        }

        public Dictionary<CellColor, int> this[int column]
        {
            get => _collapsedByColumns[column];
            set => _collapsedByColumns[column] = value;
        }

        public int Total
        {
            get { return _collapsedByColumns.SelectMany(s => s.Value.Values).Sum(); }
        }

        public IEnumerator<KeyValuePair<int, Dictionary<CellColor, int>>> GetEnumerator()
        {
            return _collapsedByColumns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Append(CellItem killed)
        {
            this[killed.Position.X][killed.Tag]++;
        }

        public void Append(List<CellItem> killed)
        {
            killed.ForEach(k => this[k.Position.X][k.Tag]++);
        }

        public void Append(CollapseResult result)
        {
            foreach (var pair in result)
            foreach (var color in pair.Value)
                this[pair.Key][color.Key] += color.Value;
        }
    }
}