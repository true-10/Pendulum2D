using System;
using System.Collections.Generic;
using System.Linq;
using True10.GridSystem;
using UnityEngine;

namespace Pendulum2D
{
    public static class CheckMatchesProcessor
    {

        public static void CheckMatches(List<CellInfo<CircleObject>> cellInfos, Action<List<CellInfo<CircleObject>>> onFindMathces )
        {
            for (int i = 0; i < cellInfos.Count; i++)
            {
                var verticalLine = cellInfos
                    .Where(x => x.GridCell.Coordinates.z == i)
                    .Where(x => x.Object != null)
                    .ToList();
                if (CheckList(verticalLine))
                {
                    onFindMathces?.Invoke(verticalLine);
                    return;
                }
                var horizontalLine = cellInfos
                    .Where(x => x.GridCell.Coordinates.x == i)
                    .Where(x => x.Object != null)
                    .ToList();
                if (CheckList(horizontalLine))
                {
                    onFindMathces?.Invoke(horizontalLine);
                    return;
                }
            }
            var diagonalLine = cellInfos
                .Where(x => x.GridCell.Coordinates.x == x.GridCell.Coordinates.z)
                .Where(x => x.Object != null)
                .ToList();
            if (CheckList(diagonalLine))
            {
                onFindMathces?.Invoke(diagonalLine);
                return;
            }
            const int xMax = 2;
            const int zMax = 2;
            var diagonalLine2 = cellInfos
                .Where(x => (x.GridCell.Coordinates.x == xMax - x.GridCell.Coordinates.z) && (x.GridCell.Coordinates.z == zMax - x.GridCell.Coordinates.x))
                .Where(x => x.Object != null)
                .ToList();
            if (CheckList(diagonalLine2))
            {
                onFindMathces?.Invoke(diagonalLine2);
                return;
            }
            onFindMathces?.Invoke(null);
        }

        private static bool CheckList(List<CellInfo<CircleObject>> cellInfos)
        {
            if (cellInfos.Any(c => c.Object == null) || cellInfos.Count < 3)
            {
                return false;
            }
            int type = cellInfos[0].Object.TypeId;
            return cellInfos.All(x => x.Object.TypeId == type);
        }
        
    }
}
