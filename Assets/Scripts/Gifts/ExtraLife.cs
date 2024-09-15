using EventMessages;
using MessagePipe;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    private IPublisher<ExtraLifeGained> _extraLifeGainedPublisher;

    public void Initialize(IPublisher<ExtraLifeGained> extraLifeGainedPublisher)
    {
        _extraLifeGainedPublisher = extraLifeGainedPublisher;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            _extraLifeGainedPublisher.Publish(new ExtraLifeGained());
            Destroy(gameObject);
        }
    }
}
