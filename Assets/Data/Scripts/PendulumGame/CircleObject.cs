using True10.Prototyping;
using UnityEngine;

namespace Pendulum2D
{
    [System.Serializable]
    public class CircleStaticData
    {
        public int TypeId;
        public int Points;
    }

    [System.Serializable]
    public class CircleObject : MonoBehaviour
    {
        [SerializeField]
        private CircleStaticData staticData;
        [SerializeField]
        private GameObject onDestroyPSPrefab;

        public int TypeId => staticData.TypeId;
        public int Points => staticData.Points;
        public Rigidbody2D Rigidbody => cachedRigidbody;

        private Rigidbody2D cachedRigidbody;
        private FollowTransform followTransform;

        public void SetRbType(RigidbodyType2D rbType) => cachedRigidbody.bodyType = rbType;
        public void SetFollow(bool follow) => followTransform.enabled = follow;
        private void Awake() => InitIfNeeded();

        private void InitIfNeeded()
        {
            if (cachedRigidbody == null)
            {
                cachedRigidbody = GetComponent<Rigidbody2D>();
            }
            if (followTransform == null)
            {
                followTransform = GetComponent<FollowTransform>();
            }
        }

        public void Init(Transform target)
        {
            followTransform.SetTarget(target);
            ResetCircle();
        }

        public void ResetCircle()
        {
            SetFollow(false);
            SetRbType(RigidbodyType2D.Kinematic);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void SetMeFree()
        {
            SetFollow(false);
            SetRbType(RigidbodyType2D.Dynamic);
        }

        public void Destroy()
        {
            if (onDestroyPSPrefab != null)
            {
                var hitVfx = Instantiate(onDestroyPSPrefab, cachedRigidbody.position, Quaternion.identity);
                hitVfx.DestroyVFXGameObject();
            }

            SetRbType(RigidbodyType2D.Static);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
