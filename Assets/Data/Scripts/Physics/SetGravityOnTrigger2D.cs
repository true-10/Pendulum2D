using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendulum2D
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SetGravityOnTrigger2D : MonoBehaviour
    {
        [SerializeField]
        private float gravityValueOnEnter = 0f;
        [SerializeField]
        private float gravityValueOnExit = 1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb != null)
            {
                rb.gravityScale = gravityValueOnEnter;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb != null)
            {
                rb.gravityScale = gravityValueOnExit;
            }
        }
    }
}

