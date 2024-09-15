using EventMessages;
using MessagePipe;
using UnityEngine;

public class CreatorGifts : MonoBehaviour
{
    private ExtraLife _extraLifePrefab;
    private IPublisher<ExtraLifeGained> _extraLifeGainedPublisher;

    public void Initialize(ExtraLife extraLifePrefab, IPublisher<ExtraLifeGained> extraLifeGainedPublisher)
    {
        _extraLifeGainedPublisher = extraLifeGainedPublisher;
        _extraLifePrefab = extraLifePrefab;
    }
    public void CreateGift()
    {
        var randomChance = Random.Range(1, 10);
        if (randomChance == 1)
        {
            var extraLife = Instantiate(_extraLifePrefab, transform.position, Quaternion.identity);
            extraLife.Initialize(_extraLifeGainedPublisher);
        }
        
    }
    
}