using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EventMessages;
using MessagePipe;
using VContainer.Unity;

public class GameController : IStartable, IDisposable
{
    private UIController _uiController;
    private readonly LevelsLoader _levelsLoader;
    private int _currentLevel;
    private readonly IDisposable _subscriptions;
    
    public GameController(UIController uiController, LevelsLoader levelsLoader,
        ISubscriber<LevelFinished> levelFinishedSubscriber)
    {
        _levelsLoader = levelsLoader;
        _uiController = uiController;
        _subscriptions = levelFinishedSubscriber.Subscribe(_ => LoadNewLevel().Forget());
    }

    public void Start()
    {
        LoadNewLevel().Forget();
    }

    private async UniTask LoadNewLevel()
    {
       await _levelsLoader.LoadLevel(_currentLevel);
        _currentLevel++;
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}