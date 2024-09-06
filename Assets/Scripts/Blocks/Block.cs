using EventMessages;
using MessagePipe;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int _amountPoints;
    private IPublisher<BlockDestroyed> _blockDestroyedPublisher;

    public void Initialize(int amountPoints, IPublisher<BlockDestroyed> blockDestroyedPublisher)
    {
        _blockDestroyedPublisher = blockDestroyedPublisher;
        _amountPoints = amountPoints;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GlobalConstants.BALL_TAG))
        {
            _blockDestroyedPublisher.Publish(new BlockDestroyed(_amountPoints));
            Destroy(gameObject);
        }
    }
}
