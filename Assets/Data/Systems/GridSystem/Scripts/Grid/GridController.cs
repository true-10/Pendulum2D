using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace True10.GridSystem
{
    public sealed class GridController : IGridController
    {
        public Action<GridCell> OnCellEnter { get; set; }
        public Action<GridCell> OnCellExit { get; set; }
        public Action<GridCell> OnCellOver { get; set; }

        public Grid Grid { get; private set; }

        private GridCell currentCell;
        private GridCell previousCell;

        public GridController(Grid grid)
        {
            Grid = grid;
        }

        public void CheckPosition(Vector3 positionOnGrid, Action<GridCell> onHit)
        {
            var cell = Grid.GetCellByPosition(positionOnGrid);
            CallbacksUpdate(cell);
            onHit?.Invoke(cell);
        }

        private void CallbacksUpdate(GridCell cell)
        {
            if (cell != null)
            {
                if (currentCell == null)
                {
                    currentCell = cell;
                    OnCellEnter?.Invoke(cell);
                }
                else //if (currentCell != null)
                {
                    if (currentCell == previousCell)
                    {
                        OnCellOver?.Invoke(cell);
                    }
                    else
                    {
                        previousCell = currentCell;
                        currentCell = cell;
                        OnCellExit?.Invoke(previousCell);
                        OnCellEnter?.Invoke(currentCell);
                    }
                }
            }
        }

    }
}
