using System;

namespace True10.GridSystem
{
    public interface IGridInput
    {
        Action<GridCell> OnInput { get; set; }
        void Tick();
    }

}
