using System;
using DG.Tweening;
using EventMessages;
using JetBrains.Annotations;
using MessagePipe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private HealthControllerView _healthControllerView;
    [SerializeField] private Button _controlChangeButton;
    [SerializeField] private TextMeshProUGUI _gameOverMessage;
    [SerializeField] private Image _startScreen;
    [SerializeField] private ScoreView _scoreView;

    private IDisposable _subscriptions;

    [Inject]
    public void Construct(Player.Player player, ISubscriber<GameOverMessage> gameOverSubscriber, IScore scoreController)
    {
        _subscriptions = DisposableBag.Create(gameOverSubscriber.Subscribe(_ => DisableControlSwitchButton()),
            gameOverSubscriber.Subscribe(_ => ShowMessage()));
        _healthControllerView.Initialize(player.HealthController);
        _scoreView.Initialize(scoreController);
    }

    [UsedImplicitly]
    public void HideStartMenu()
    {
        _startScreen.gameObject.SetActive(false);
    }

    private void ShowMessage()
    {
        _gameOverMessage.transform.DOLocalMoveX(1642, 5);
    }

    private void DisableControlSwitchButton()
    {
        _controlChangeButton.interactable = false;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}