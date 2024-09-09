using System;
using System.Collections.Generic;
using EventMessages;
using MessagePipe;
using UnityEngine;
using VContainer;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks;
    private bool _isActive;
    private int _countBlocks;
    private IDisposable _subscriptions;
    private IPublisher<LevelFinished> _levelFinishedPublisher;


    public void Initialize(IPublisher<BlockDestroyed> blockDestroyedPublisher,
        ISubscriber<BlockDestroyed> blockDestroyedSubscriber,
        IPublisher<LevelFinished> levelFinishedPublisher, BlockConfig blockConfig,
        IPublisher<ExtraLifeGained> extraLifeGainedPublisher)
    {
        _levelFinishedPublisher = levelFinishedPublisher;

        foreach (var block in _blocks)
        {
            block.Initialize(blockDestroyedPublisher, blockConfig, extraLifeGainedPublisher);
        }

        _subscriptions = blockDestroyedSubscriber.Subscribe(_ => UpdateBlockCount());
    }

    public void EnableLevel()
    {
        _isActive = true;
        _countBlocks = _blocks.Count;
    }

    private void UpdateBlockCount()
    {
        if (!_isActive)
        {
            return;
        }

        _countBlocks--;

        if (_countBlocks <= 0)
        {
            _isActive = false;
            gameObject.SetActive(false);
            _levelFinishedPublisher.Publish(new LevelFinished());
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}