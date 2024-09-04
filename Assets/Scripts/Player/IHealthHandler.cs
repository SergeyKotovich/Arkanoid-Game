using System;

namespace Player
{
    public interface IHealthHandler
    {
        public event Action HealthChanged;
    }
}