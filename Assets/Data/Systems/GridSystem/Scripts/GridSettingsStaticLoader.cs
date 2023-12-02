using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace True10.GridSystem
{
    public static class GridSettingsStaticLoader
    {
        const string GridSettingsPath = "Grid Settings SO";

        public static GridSettingsSO LoadSettings()
        {
            return Resources.Load<GridSettingsSO>(GridSettingsPath);
        }
    }
}
