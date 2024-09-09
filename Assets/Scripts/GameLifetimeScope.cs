using System.Collections.Generic;
using Ball;
using EventMessages;
using MessagePipe;
using Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Player.Player _player;
    [SerializeField] private Boundary _boundary;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Ball.Ball _ball;
    [SerializeField] private BallConfig _ballConfig;
    [SerializeField] private BlockConfig _blockConfig;
    [SerializeField] private UIController _uiController;
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private List<Level> _listLevels;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterMessagePipe(builder);
        
        builder.Register<InputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ScoreController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        
        builder.RegisterInstance(_player).AsImplementedInterfaces();
        builder.RegisterInstance(_boundary);
        builder.RegisterInstance(_playerConfig);
        builder.RegisterInstance(_mainCamera);
        builder.RegisterInstance(_ball);
        builder.RegisterInstance(_ballConfig);
        builder.RegisterInstance(_blockConfig);
        builder.RegisterInstance(_uiController);
        builder.RegisterInstance(_levelsLoader);
        builder.RegisterInstance(_listLevels);

        builder.RegisterEntryPoint<GameController>();
    }

    private void RegisterMessagePipe(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

        builder.RegisterMessageBroker<BallHitBottomMessage>(options);
        builder.RegisterMessageBroker<GameOverMessage>(options);
        builder.RegisterMessageBroker<BlockDestroyed>(options);
        builder.RegisterMessageBroker<LevelFinished>(options);
    }
    
}