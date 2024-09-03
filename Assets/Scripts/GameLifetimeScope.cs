using MessagePipe;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    protected override void Configure(IContainerBuilder builder)
    {
        RegisterInput(builder);
        RegisterMessagePipe(builder);
    }

    private void RegisterMessagePipe(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c=>GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
    }
    private void RegisterInput(IContainerBuilder builder)
    {
        if (Application.isMobilePlatform)
        {
            _leftButton.gameObject.SetActive(true);
            _rightButton.gameObject.SetActive(true);

            builder.RegisterInstance(_leftButton);
            builder.RegisterInstance(_rightButton);

            builder.Register<MobilInputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        else
        {
            builder.Register<StandaloneInputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
