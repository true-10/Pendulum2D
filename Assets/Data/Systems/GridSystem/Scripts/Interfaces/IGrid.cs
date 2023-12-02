using UnityEngine;

namespace True10.GridSystem
{
    public interface IGrid
    {
        Vector3Int GridSize { get; }
        GridCell GetCellByPosition(Vector3 point);
        GridCell GetCellFromIndicies(int xInd, int yInd);
    }


}

