using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace True10.Prototyping
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;
        [SerializeField]
        private bool followPosition = true;
        [SerializeField]
        private bool followRotaion = false;


        private Transform cachedTransform;
        public void SetTarget(Transform newTarget) => targetTransform = newTarget;

        private void OnValidate()
        {
            cachedTransform = GetComponent<Transform>();
        }        
        
        private void Start()
        {
            cachedTransform = GetComponent<Transform>();
        }

        void LateUpdate()
        {
            Follow();
        }


        [ContextMenu("Follow Now")]
        private void Follow()
        {
            if (followPosition)
            {
                cachedTransform.position = targetTransform.position;
            }
            if (followRotaion)
            {
                cachedTransform.rotation = targetTransform.rotation;
            }
        }
    }
}
