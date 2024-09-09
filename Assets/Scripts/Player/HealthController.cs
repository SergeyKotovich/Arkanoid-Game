using System;
using EventMessages;
using MessagePipe;
using Player;

public class HealthController : IDisposable, IHealthHandler
{
    public event Action HealthDecreased;
    public event Action LifeGained;

    private int _maxCountLives = 3;
    private int _countLives;
    private readonly IDisposable _subscriptions;
    private readonly IPublisher<GameOverMessage> _gameOverPublisher;

    public HealthController(int countLives, ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber,
        IPublisher<GameOverMessage> gameOverPublisher, ISubscriber<ExtraLifeGained> extraLifeGainedSubscriber)
    {
        _gameOverPublisher = gameOverPublisher;
        _countLives = countLives;
        _subscriptions = DisposableBag.Create(
            ballHitBottomSubscriber.Subscribe(_ => OnBallHitBottom()),
            extraLifeGainedSubscriber.Subscribe(_ => UpdateCountLives())
        );


    }

    private void UpdateCountLives()
    {
        if (_countLives<_maxCountLives)
        {
            _countLives++;
            LifeGained?.Invoke();
        }
    }

    private void OnBallHitBottom()
    {
        _countLives -= 1;
        HealthDecreased?.Invoke();

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