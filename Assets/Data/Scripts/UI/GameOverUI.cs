using TMPro;
using UnityEngine;
using Zenject;

namespace Pendulum2D
{
    public class GameOverUI : MonoBehaviour
    {
        [Inject]
        private PlayerData playerData;

        [SerializeField]
        private TextMeshProUGUI pointsText;
        private void OnEnable()
        {
            pointsText.text = $"{playerData.Points.Value} points";
        }
    }

}
