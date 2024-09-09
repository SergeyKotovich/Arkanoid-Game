using System.Linq;
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
        var extraLife = Instantiate(_extraLifePrefab, transform.position, Quaternion.identity);
        extraLife.Initialize(_extraLifeGainedPublisher);
    }
    
}