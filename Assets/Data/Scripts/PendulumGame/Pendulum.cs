using System.Collections;
using UnityEngine;

namespace Pendulum2D
{

    public class Pendulum : MonoBehaviour
    {
        public Transform HookTransform => hookTransform;

        [SerializeField]
        private Transform pivotTransform;
        [SerializeField]
        private Transform hookTransform;

        [SerializeField]
        private float maxAngleDeflection = 30.0f;
        [SerializeField]
        private float startSpeedOfPendulum = 1.0f;


        private float speedOfPendulum = 1.0f;
        private float increaseSpeedRateSec = 10f;
        private float timeToSpeedUp;

        private void Start()
        {
            speedOfPendulum = startSpeedOfPendulum;
            timeToSpeedUp = 0f;
        }

        private void UpdateCirclePosition()
        {

            float angle = maxAngleDeflection * Mathf.Sin(Time.time * speedOfPendulum);
            pivotTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }


        void Update()
        {
            UpdateCirclePosition();
            //UpdateSpeed();
        }

        void UpdateSpeed()
        {
            timeToSpeedUp += Time.deltaTime;
            
            if (timeToSpeedUp >= increaseSpeedRateSec)
            {
                speedOfPendulum += 1f;
                timeToSpeedUp = 0f;
            }
        }
    }
}
