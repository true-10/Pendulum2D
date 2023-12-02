using System;
using UnityEngine;

namespace True10.GridSystem
{
    public interface IGridController
    {
        Action<GridCell> OnCellEnter { get; set; }
        Action<GridCell> OnCellExit { get; set; }
        Action<GridCell> OnCellOver { get; set; }
        Grid Grid { get; }
        void CheckPosition(Vector3 positionOnGrid, Action<GridCell> onHit);
    }
}
