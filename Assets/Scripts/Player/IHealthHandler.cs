using System;

namespace Player
{
    public interface IHealthHandler
    {
        public event Action HealthDecreased;
        public event Action LifeGained;
    }
}