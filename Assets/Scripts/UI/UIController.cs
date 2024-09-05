using System;
using DG.Tweening;
using EventMessages;
using MessagePipe;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private HealthControllerView _healthControllerView;
    [SerializeField] private Button _controlChangeButton;
    [SerializeField] private TextMeshProUGUI _gameOverMessage;

    private IDisposable _subscriptions;

    [Inject]
    public void Construct(Player.Player player, ISubscriber<GameOverMessage> gameOverSubscriber)
    {
        _subscriptions = DisposableBag.Create(gameOverSubscriber.Subscribe(_ => DisableControlSwitchButton()),
            gameOverSubscriber.Subscribe(_ => ShowMessage()));
        _healthControllerView.Initialize(player.HealthController);
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