using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendulum2D
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BalloonDestroyer : MonoBehaviour
    {
        [SerializeField]
        private GameObject psPrefab;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var rb = collision.attachedRigidbody;
            if (rb != null)
            {
                if (rb.gameObject.TryGetComponent(out CircleObject circle))
                {
                    circle.Destroy();
                }
                else
                {
                    rb.gameObject.SetActive(false);
                    if (psPrefab != null)
                    {
                        var hitVfx = Instantiate(psPrefab, rb.position, Quaternion.identity);
                        hitVfx.DestroyVFXGameObject();
                    }
                }
            }
        }
    }
}
