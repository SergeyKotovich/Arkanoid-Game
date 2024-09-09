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
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Image _startScreen;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private TextMeshProUGUI _currentLevelLabel;

    private IDisposable _subscriptions;

    [Inject]
    public void Construct(Player.Player player, ISubscriber<GameOverMessage> gameOverSubscriber, IScore scoreController)
    {
        _subscriptions = DisposableBag.Create(gameOverSubscriber.Subscribe(_ => DisableControlSwitchButton()),
            gameOverSubscriber.Subscribe(_ => ShowDefeatScreen()));
        _healthControllerView.Initialize(player.HealthController);
        _scoreView.Initialize(scoreController);
    }

    [UsedImplicitly]
    public void HideStartMenu()
    {
        _startScreen.gameObject.SetActive(false);
    }

    public void ShowVictoryScreen()
    {
        _victoryScreen.SetActive(true);
    }
    public void ShowCurrentLevelMessage(int currentLevel)
    {
        _currentLevelLabel.text = currentLevel.ToString();
    }

    private void ShowDefeatScreen()
    {
       _gameOverScreen.SetActive(true);
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