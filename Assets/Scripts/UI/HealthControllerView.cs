using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using VContainer;

public class HealthControllerView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listLives;

    private IHealthHandler _healthController;
    
    public void Initialize(IHealthHandler healthController)
    {
        _healthController = healthController;
        _healthController.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        var activeLife = _listLives.FirstOrDefault(life => life.activeSelf);
        
        if (activeLife != null)
        {
            activeLife.SetActive(false); 
        }
    }

    private void OnDestroy()
    {
        _healthController.HealthChanged -= OnHealthChanged;
    }
}