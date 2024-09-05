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

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterMessagePipe(builder);
        
        builder.Register<InputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        
        builder.RegisterInstance(_player).AsImplementedInterfaces();
        builder.RegisterInstance(_boundary);
        builder.RegisterInstance(_playerConfig);
        builder.RegisterInstance(_mainCamera);
        builder.RegisterInstance(_ball);
        builder.RegisterInstance(_ballConfig);
    }

    private void RegisterMessagePipe(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

        builder.RegisterMessageBroker<BallHitBottomMessage>(options);
        builder.RegisterMessageBroker<GameOverMessage>(options);
    }
    
}