using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Pendulum2D
{
    public class Buckets : MonoBehaviour
    {
        [SerializeField]
        private List<Bucket> buckets = new();

        public Action<int, int, CircleObject> OnCircleAdded { get; set; }
        public Action<int, int, CircleObject> OnCircleRemoved { get; set; }

        public Bucket GetBucket(int column) => buckets.FirstOrDefault(x => x.Column == column);
        public bool AreFull => buckets.All(b => b.IsFull);

        private void Start()
        {
            buckets.ForEach(b =>
            { 
                b.OnCircleAdded += OnCircleAddedHandler; 
                b.OnCircleRemoved += OnCircleRemovedHandler;             
            });
        }

        public void Clear() => buckets.ForEach(b => b.Clear());

        private void OnCircleAddedHandler(int col, int row, CircleObject circle) => OnCircleAdded?.Invoke(col, row, circle);

        private void OnCircleRemovedHandler(int col, int row, CircleObject circle) => OnCircleRemoved?.Invoke(col, row, circle);

    }
}
