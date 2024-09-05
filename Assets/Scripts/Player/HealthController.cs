using System;
using EventMessages;
using MessagePipe;
using Player;

public class HealthController : IDisposable, IHealthHandler
{
    public event Action HealthChanged;

    private int _countLives;
    private readonly IDisposable _subscriptions;
    private readonly IPublisher<GameOverMessage> _gameOverPublisher;

    public HealthController(int countLives, ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber,
        IPublisher<GameOverMessage> gameOverPublisher)
    {
        _gameOverPublisher = gameOverPublisher;
        _countLives = countLives;
        _subscriptions = ballHitBottomSubscriber.Subscribe(_ => OnBallHitBottom());
    }

    private void OnBallHitBottom()
    {
        _countLives -= 1;
        HealthChanged?.Invoke();

        if (_countLives <= 0)
        {
            _gameOverPublisher.Publish(new GameOverMessage());
        }
    }


    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}