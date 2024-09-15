using EventMessages;
using MessagePipe;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int _amountPoints;
    private IPublisher<BlockDestroyed> _blockDestroyedPublisher;
    private BlockConfig _blockConfig;
    private IPublisher<ExtraLifeGained> _extraLifeGainedPublisher;
    private CreatorGifts _creatorGifts;

    public void Initialize(IPublisher<BlockDestroyed> blockDestroyedPublisher, BlockConfig blockConfig,
        IPublisher<ExtraLifeGained> extraLifeGainedPublisher)
    {
        _extraLifeGainedPublisher = extraLifeGainedPublisher;
        _blockConfig = blockConfig;
        _amountPoints = blockConfig.AmountPoints;
        _blockDestroyedPublisher = blockDestroyedPublisher;
        _creatorGifts = GetComponent<CreatorGifts>();
        _creatorGifts.Initialize(_blockConfig.ExtraLifePrefab, _extraLifeGainedPublisher);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GlobalConstants.BALL_TAG))
        {
            {
                _blockDestroyedPublisher.Publish(new BlockDestroyed(_amountPoints));
                Destroy(gameObject);
                _creatorGifts.CreateGift();
            }
        }
    }
}