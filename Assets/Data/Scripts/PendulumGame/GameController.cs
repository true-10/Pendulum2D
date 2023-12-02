using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Zenject;
using True10.GridSystem;
using Grid = True10.GridSystem.Grid;
using System.Linq;

namespace Pendulum2D
{

    [Serializable]
    public class GridDebugIngfo
    {
        public List<CellInfo<CircleObject>> cellInfos;

    }

    //todo
    //по завершение игры прилетает вертолет и взрывается

    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private Pendulum pendulum;
        [SerializeField]
        private Buckets buckets;
        [SerializeField]
        private UIManager uiManager;
        [SerializeField]
        private CircleSpawner circleSpawner;

        [Inject]
        private GridInfoManager<CircleObject> gridInfoManager;
        [Inject]
        private IGridController gridController;
        [Inject]
        private PlayerData playerData;

        private CircleObject current;

        private bool IsGameEnded() 
        {
            return buckets.AreFull
                || ( circleSpawner.ObjectList.All(x => x.activeInHierarchy == true) && current == null);
        }

        private void Start()
        {
            InitGame(); 
        }

        public void InitGame()
        {
            circleSpawner.Init();
            
            var allCellInfos = gridInfoManager.GridInfo.GetAllCellInfos();
            for (int i = 0; i < 3; i++)
            {
                var cellInfos = allCellInfos.Where(x => x.GridCell.Coordinates.x == i).ToList();
                buckets.GetBucket(i).SetCellInfos(cellInfos);
            }
            buckets.OnCircleAdded += OnCircleAdded;
            buckets.OnCircleRemoved += OnCircleRemoved;
            uiManager.OpenStartMenu();
        }

        public void StartGame()
        {
            playerData.Points.Value = 0;
            gridInfoManager.GridInfo.Reset();
            circleSpawner.ResetCircles();
            uiManager.OpenGameUI();
            buckets.Clear();
            PrepareNextCircle();
        }

        private void OnCircleAdded(int col, int row, CircleObject circle)
        {
            var cellInfos = gridInfoManager.GridInfo.GetAllCellInfos();
            CheckMatchesProcessor.CheckMatches(cellInfos, OnFindMatches);
        }

        private void OnFindMatches(List<CellInfo<CircleObject>> cellInfos)
        {
            if (cellInfos == null || cellInfos.Count < 3)
            {
                CheckEndGame();
                return;
            }
            cellInfos = cellInfos.OrderByDescending(x => x.GridCell.Coordinates.z).ToList();

            foreach (var cellInfo in cellInfos)
            {
                var circle = cellInfo.Object;
                if (circle == null)
                {
                    continue;
                }
                var column = cellInfo.GridCell.Coordinates.x;
                var bucket = buckets.GetBucket(column);

                bucket.Remove(circle);
            }
        }

        private void OnCircleRemoved(int col, int row, CircleObject circle)
        {
            playerData.Points.Value += circle.Points;
        }

        private void CheckEndGame()
        {
            if (IsGameEnded())
            {
                current?.SetMeFree();
                uiManager.OpenGameOverUI();
            }
        }

        public void ReleaseCircleFromPendulum()
        {
            if (current != null)
            {
                current.SetMeFree();
                current = null;
                PrepareNextCircle();
            }
            CheckEndGame();
        }

        public void PrepareNextCircle()
        {
            var nextCircle = circleSpawner.GetNextCircle();
            if (nextCircle == null)
            {
                return;
            }
            var hookTransfrom = pendulum.HookTransform;
            nextCircle.gameObject.SetActive(true);
            nextCircle.transform
                .DOScale(0f, 0.0f);

            nextCircle.transform
                .DOScale(0f, 0.3f)//just a small delay
                .OnComplete(() =>
                {
                    nextCircle.SetFollow(true);
                    nextCircle.transform
                        .DOScale(1.3f, 0.3f)
                        .OnComplete(() =>
                        {
                            current = nextCircle;
                        });
                });
        }
    }
}
