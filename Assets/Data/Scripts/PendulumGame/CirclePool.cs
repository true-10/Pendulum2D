using System.Collections.Generic;
using System.Linq;

namespace Pendulum2D
{
    public class CirclePool // <T>
    {
        private List<CircleObject> circles;

        public CirclePool(List<CircleObject> circles)
        {
            this.circles = circles;
        }

        public CircleObject GetNextCircle()
        {
            var inactiveCircles = circles
                .Where(circle => circle.gameObject.activeInHierarchy == false)
                .ToList();

            return inactiveCircles.GetRandomElement<CircleObject>();
        }

        public void ResetCircles()
        {
            foreach (var circle in circles)
            {
                circle.ResetCircle();
            }
        }

    }
}
