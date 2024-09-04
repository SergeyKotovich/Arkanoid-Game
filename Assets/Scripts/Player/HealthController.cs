using System;
using EventMessages;
using MessagePipe;
using Player;

public class HealthController : IDisposable, IHealthHandler
{
    public event Action HealthChanged;

    private int _countLives;
    private readonly IDisposable _subscribers;

    public HealthController(int countLives, ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber)
    {
        _countLives = countLives;
        _subscribers = ballHitBottomSubscriber.Subscribe(_ => OnBallHitBottom());
    }

    private void OnBallHitBottom()
    {
        _countLives -= 1;
        HealthChanged?.Invoke();
    }


    public void Dispose()
    {
        _subscribers.Dispose();
    }
}