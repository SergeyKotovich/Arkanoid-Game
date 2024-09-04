using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using VContainer;

public class HealthControllerView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listLives;

    private IHealthHandler _healthController;

    [Inject]
    public void Construct(Player.Player player)
    {
        _healthController = player.HealthController;
        _healthController.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        var live = _listLives.Last();
        live.gameObject.SetActive(false);
    }
}