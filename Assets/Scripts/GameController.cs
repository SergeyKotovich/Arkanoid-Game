using System;
using Cysharp.Threading.Tasks;
using EventMessages;
using MessagePipe;
using VContainer.Unity;

public class GameController : IStartable, IDisposable
{
    private readonly UIController _uiController;
    private readonly LevelsLoader _levelsLoader;
    private int _currentLevel;
    private readonly IDisposable _subscriptions;
    private int _countLevels;

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
        _countLevels = _levelsLoader.GetCountLevels();
        if (_currentLevel < _countLevels)
        {
            await _levelsLoader.LoadLevel(_currentLevel);
            _uiController.ShowCurrentLevelMessage(_currentLevel + 1);
            _currentLevel++;
        }
        else
        {
            GameFinished();
        }
    }

    private void GameFinished()
    {
        _uiController.ShowVictoryScreen();
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}