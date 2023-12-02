using System;

namespace True10.GridSystem
{
    public interface IGridGenerator 
    {
        Grid Generate(GridData gridData, Action<GridCell> onCellCreated = null);

    }
}
