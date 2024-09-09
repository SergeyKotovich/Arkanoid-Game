using EventMessages;
using MessagePipe;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int _amountPoints = 10;
    private IPublisher<BlockDestroyed> _blockDestroyedPublisher;

    public void Initialize(IPublisher<BlockDestroyed> blockDestroyedPublisher)
    {
        _blockDestroyedPublisher = blockDestroyedPublisher;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GlobalConstants.BALL_TAG))
        {
            {
                _blockDestroyedPublisher.Publish(new BlockDestroyed(_amountPoints));
                Destroy(gameObject);
            }
        }
    }
}