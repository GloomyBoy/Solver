using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmPuzzleLogic.Analyze;

namespace EmPuzzleLogic.Entity
{
    public class CollapseResult : IEnumerable<KeyValuePair<int, int>>
    {
        private Dictionary<int, int> _collapsedByColumns = new Dictionary<int, int>(); 
        public CollapseResult(int width)
        {
            foreach (var i in Enumerable.Range(0, width))
            {
                _collapsedByColumns[i] = 0;
            }
        }

        public int this[int column]
        {
            get { return _collapsedByColumns[column]; }
            set { _collapsedByColumns[column] = value; }
        }

        public void Append(CellItem killed)
        {
            this[killed.Position.X]++;
        }

        public void Append(List<CellItem> killed)
        {
            killed.ForEach(k => this[k.Position.X]++);
        }

        public void Append(CollapseResult result)
        {
            foreach (var pair in result)
            {
                this[pair.Key] += pair.Value;
            }
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return _collapsedByColumns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}