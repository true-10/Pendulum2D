using System.Collections.Generic;

namespace True10.GridSystem
{
    [System.Serializable]
    public sealed class GridInfo<T>
    {
        private Dictionary<int, CellInfo<T>> cellInfos;

        public List<GridCell> AvailableCells { get; set; }
        public bool IsNoMoreMoves() => AvailableCells.Count == 0;

        public void Reset()
        {
            if (cellInfos == null)
            {
                return;
            }
            Dictionary<int, CellInfo<T>> newCellInfos = new(cellInfos);
            cellInfos = new();
            foreach (var cellInfo in newCellInfos.Values)
            {
                cellInfo.Object = default(T);
                UpdateCellInfo(cellInfo.GridCell, cellInfo);
            }
        }

        public List<CellInfo<T>> GetAllCellInfos()
        {
            List<CellInfo<T>> result = new();
            foreach (var cellinfo in cellInfos.Values)
            {
                result.Add(cellinfo);
            }
            return result;
        }

        public CellInfo<T> GetCellInfo(GridCell gridCell)
        {
            if (gridCell == null)
            {
                return null;
            }
            return GetCellInfo(gridCell.Index);
        }

        public CellInfo<T> GetCellInfo(int index)
        {
            if (cellInfos == null || !cellInfos.ContainsKey(index))
            {
                return null;
            }
            return cellInfos[index];
        }

        public void UpdateCellInfo(GridCell gridCell, CellInfo<T> cellInfo) 
        {
            UpdateCellInfo(gridCell.Index, cellInfo);
        }

        public void UpdateCellInfo(int index, CellInfo<T> cellInfo)
        {
            if (cellInfos == null)
            {
                cellInfos = new();
            }
            if (cellInfos.ContainsKey(index))
            {
                cellInfos[index] = cellInfo;
                return;
            }

            cellInfos.Add(index, cellInfo);
        }

    }
}

