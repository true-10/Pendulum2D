using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendulum2D
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject startMenuUI;
        [SerializeField]
        private GameObject gameUI;
        [SerializeField]
        private GameObject gameOverMenuUI;

        public void HideAll()
        {
            startMenuUI.SetActive(false);
            gameUI.SetActive(false);
            gameOverMenuUI.SetActive(false);
        }

        public void OpenGameUI()
        {
            HideAll();
            gameUI.SetActive(true);
        }
        public void OpenStartMenu()
        {
            HideAll();
            startMenuUI.SetActive(true);
        }

        public void OpenGameOverUI()
        {
            HideAll();
            gameOverMenuUI.SetActive(true);
        }
    }
}
