using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pendulum2D
{
    public class CameraSizeAdjuster : MonoBehaviour
    {
        const float defaultSize = 6f;


        void Start()
        {
            Camera.main.orthographicSize = GetOrhoSize();
        }

        private float GetOrhoSize()
        {
            var aspectRatio = Screen.width / (float)Screen.height;

            if (aspectRatio > 0.6)
            {
                return 4.5f;
            }

            return defaultSize;
        }
    }
}
