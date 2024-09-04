using EventMessages;
using MessagePipe;
using UnityEngine;

public class BottomBoundary : MonoBehaviour
{
    private IPublisher<BallHitBottomMessage> _ballHitBottomPublisher;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(GlobalConstants.BALL_TAG))
        {
            _ballHitBottomPublisher.Publish(new BallHitBottomMessage());
        }
    }

    public void Initialize(IPublisher<BallHitBottomMessage> ballHitBottomPublisher)
    {
        _ballHitBottomPublisher = ballHitBottomPublisher;
    }
}