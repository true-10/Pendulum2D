using UniRx;

namespace Pendulum2D
{
    public class PlayerData
    {
        public ReactiveProperty<int> Points;

        public PlayerData()
        {
            Points = new(0);
        }

        public void Reset()
        {
            Points.Value = 0;
        }
    }
}
