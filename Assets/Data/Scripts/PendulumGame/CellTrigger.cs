using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendulum2D
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CellTrigger : MonoBehaviour
    {
        private Bucket bucket;
        private CellTrigger nextTrigger;
        private CellTrigger prevTrigger;

        public CircleObject Circle { get; private set; }

        public void Setup(Bucket bucket, CellTrigger next, CellTrigger prev)
        {
            this.bucket = bucket;
            nextTrigger = next;
            prevTrigger = prev;
        }
        public void Clear() => Circle = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb != null)
            {
                if (rb.gameObject.TryGetComponent(out CircleObject circle))
                {
                    if (prevTrigger == null 
                        || (prevTrigger != null && prevTrigger.Circle != null 
                        && prevTrigger.Circle != circle))
                    {
                        Circle = circle;
                        bucket.AddObject(circle);
                    }
                }
            }
            if(prevTrigger == null || (prevTrigger != null && prevTrigger.Circle != null) )
            {
                nextTrigger?.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Circle = null;
            nextTrigger?.gameObject.SetActive(false);
        }
    }
}
