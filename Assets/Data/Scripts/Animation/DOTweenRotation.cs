using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Pendulum2D
{
    public class DOTweenRotation : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;
        [SerializeField]
        private float duration = 1f;
        [SerializeField]
        private Vector3 rotationAxis = Vector3.forward;

        void Start()
        {
            Vector3 rotation = 360f * rotationAxis;
            targetTransform
                .DORotate(rotation, duration)
                .SetLoops(-1)
                .SetEase(Ease.Linear);
        }
    }
}
