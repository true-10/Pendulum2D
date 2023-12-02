using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendulum2D
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AddForceOnTrigger2D : MonoBehaviour
    {
        [SerializeField]
        private float force;
        [SerializeField]
        private ForceMode2D forceMode;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb != null)
            {
                rb.AddForce(transform.up * force, forceMode);
            }
        }
    }
}
