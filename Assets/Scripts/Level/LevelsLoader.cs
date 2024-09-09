using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EventMessages;
using MessagePipe;
using UnityEngine;
using VContainer;

public class LevelsLoader : MonoBehaviour
{
    private List<Level> _levels;
    private const int _delay = 500;

    [Inject]
    public void Construct(List<Level> levels, IPublisher<BlockDestroyed> blockDestroyedPublisher,
        ISubscriber<BlockDestroyed> blockDestroyedSubscriber,
        IPublisher<LevelFinished> levelFinishedPublisher)
    {
        _levels = levels;
        foreach (var level in _levels)
        {
            level.Initialize(blockDestroyedPublisher,blockDestroyedSubscriber,levelFinishedPublisher);
        }
    }
    public async UniTask LoadLevel(int indexLevel)
    {
       await UniTask.Delay(_delay);
       _levels[indexLevel].gameObject.SetActive(true);
       _levels[indexLevel].EnableLevel();
    }
    
}