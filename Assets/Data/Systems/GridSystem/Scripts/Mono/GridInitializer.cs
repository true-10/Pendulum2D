using Pendulum2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace True10.GridSystem
{
    public class GridInitializer : MonoBehaviour
    {
        [Inject]
        private IGridController gridController;
        [Inject]
        private GridInfoManager<CircleObject> gridInfoManager;

        void Awake()
        {
            gridInfoManager.Init(gridController.Grid.Cells);
        }
    }
}
