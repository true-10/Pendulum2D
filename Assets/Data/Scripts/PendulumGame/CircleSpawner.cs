using System;
using System.Collections.Generic;
using True10.Prototyping;
using UnityEngine;

namespace Pendulum2D
{
    public class CircleSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform targetForCircles;
        [SerializeField]
        private ObjectSpawner objectSpawner;
        [SerializeField, Header("Prefabs")]
        private GameObject redCirclePrefab;
        [SerializeField]
        private GameObject blueCirclePrefab;
        [SerializeField]
        private GameObject greenCirclePrefab;

        public List<GameObject> ObjectList => objectSpawner.ObjectsList;

        private CirclePool circlePool;

        public void Init() => SpawnCircles();

        public void ResetCircles() => circlePool?.ResetCircles();
        public CircleObject GetNextCircle() => circlePool.GetNextCircle();

        private void SpawnCircles()
        {
            SpawnCircles(redCirclePrefab);
            SpawnCircles(blueCirclePrefab);
            SpawnCircles(greenCirclePrefab);
            var allCirclesGO = new List<CircleObject>();

            foreach (var circleGO in objectSpawner.ObjectsList)
            {
                if (circleGO.TryGetComponent(out CircleObject circle))
                {
                    allCirclesGO.Add(circle);
                }
            }
            circlePool = new(allCirclesGO);
        }

        private void SpawnCircles(GameObject prefab)
        {
            for (int i = 0; i < 4; i++)
            {
                objectSpawner.Spawn(prefab, Vector3.zero, Quaternion.identity, OnCircleSpawn);
            }
        }

        private void OnCircleSpawn(GameObject circleGO)
        {
            if (circleGO.TryGetComponent(out CircleObject circle))
            {
                circle.Init(targetForCircles);
            }
        }
    }
}
