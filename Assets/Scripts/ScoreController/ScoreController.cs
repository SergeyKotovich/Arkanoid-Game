using System;
using EventMessages;
using MessagePipe;

public class ScoreController : IDisposable, IScore
{
    public event Action<int> ScoreChanged;
    private int _score;
    private readonly IDisposable _subscriptions;

    public ScoreController(ISubscriber<BlockDestroyed> blockDestroyedSubscriber)
    {
        _subscriptions = blockDestroyedSubscriber.Subscribe(UpdateScore);
    }

    private void UpdateScore(BlockDestroyed blockDestroyed)
    {
        _score += blockDestroyed.AmountPoints;
        ScoreChanged?.Invoke(_score);
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}