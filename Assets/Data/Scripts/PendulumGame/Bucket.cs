using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using True10.GridSystem;
using Zenject;

namespace Pendulum2D
{
    public class Bucket : MonoBehaviour
    {
        [SerializeField]
        private int gridColumn = 0;
        [SerializeField]
        private List<CellTrigger> triggers;

        [Inject]
        private GridInfoManager<CircleObject> gridInfoManager;

        public Action<int, int, CircleObject> OnCircleAdded { get; set; }
        public Action<int, int, CircleObject> OnCircleRemoved { get; set; }

        public int Column => gridColumn;
        public int Count => objectsInBucket.Count;
        public bool IsEmpty => objectsInBucket.Count == 0;
        public bool IsFull => objectsInBucket.Count == 3;


        private List<CircleObject> objectsInBucket = new();
        private List<CellInfo<CircleObject>> cellInfos;

        public void Start()
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                var current = triggers[i];

                if (i == 0)
                {
                    current.Setup(this, triggers[i + 1], null);
                    continue;
                }
                if (i == triggers.Count - 1)
                {
                    current.Setup(this, null, triggers[i - 1]);
                    continue;
                }
                current.Setup(this, triggers[i + 1], triggers[i - 1]);
            }
        }
        public void SetCellInfos(List<CellInfo<CircleObject>> cellInfos) => this.cellInfos = cellInfos;

        public void UpdateGridInfo()
        {
            for (int i = 0; i < cellInfos.Count; i++)
            {
                var cellInfo = cellInfos.FirstOrDefault(ci => ci.GridCell.Coordinates.z == i);
                if (i < objectsInBucket.Count)
                {
                    var circle = objectsInBucket[i];
                    cellInfo.Object = circle;
                }
                else
                {
                    cellInfo.Object = null;
                }
                gridInfoManager.UpdateCellInfo(cellInfo.GridCell, cellInfo);
            }
            CheckBucket();
        }

        public void Clear()
        {
            objectsInBucket.Clear();

            foreach (var cellInfo in cellInfos)
            {
                cellInfo.Object = null;
                gridInfoManager.UpdateCellInfo(cellInfo.GridCell, cellInfo);
            }

            for (int i = 0; i < triggers.Count; i++)
            {
                var trigger = triggers[i];
                trigger.Clear();
                if (i != 0)
                {
                    trigger.gameObject.SetActive(false);
                }
            }
        }

        public void Remove(CircleObject circle)
        {
            if (objectsInBucket.Contains(circle))
            {
                int row = objectsInBucket.IndexOf(circle);
                triggers.FirstOrDefault(t => t.Circle == circle)?.Clear();
                objectsInBucket.Remove(circle);
                circle.Destroy();
                UpdateGridInfo();
                OnCircleRemoved?.Invoke(gridColumn, row, circle);
            }
        }

        private void CheckBucket()
        {
            var inactiveCircles = objectsInBucket.Where(x => x.gameObject.activeInHierarchy == false).ToList();
            foreach (var c in inactiveCircles)
            {
                Remove(c);
            }
        }

        public void AddObject(CircleObject circle)
        {
            objectsInBucket.Add(circle);
            UpdateGridInfo();
            int row = objectsInBucket.Count - 1;
            OnCircleAdded?.Invoke(gridColumn, row, circle);
        }
    }
}
