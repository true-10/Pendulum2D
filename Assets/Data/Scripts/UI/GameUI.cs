using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Pendulum2D
{
    public class GameUI : MonoBehaviour
    {
        [Inject]
        private PlayerData playerData;

        [SerializeField]
        private TextMeshProUGUI pointsText;

        private IDisposable pointsSubsriber;

        private void OnEnable()
        {
            pointsSubsriber = playerData.Points
                .ObserveEveryValueChanged(x => x.Value)
                .Subscribe(points => pointsText.text = $"Pts: {points}")
                .AddTo(this);
        }

        private void OnDisable()
        {
            pointsSubsriber?.Dispose();
            pointsSubsriber = null;
        }

    }

}
