namespace True10.GridSystem
{
    [System.Serializable]
    public class CellInfo<T>
    {
        public GridCell GridCell;
        public T Object;

        public bool IsEmpty => Object == null;


    }
}

