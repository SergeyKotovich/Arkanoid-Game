using EventMessages;
using MessagePipe;
using UnityEngine;
using VContainer;

public class Boundary : MonoBehaviour
{
    public BoundaryPointsProvider BoundaryPointsProvider => _boundaryPoints;

    [SerializeField] private BoundaryPointsProvider _boundaryPoints;
    [SerializeField] private BottomBoundary _bottomBoundary;

    [Inject]
    public void Construct(IPublisher<BallHitBottomMessage> ballHitBottomPublisher)
    {
        _bottomBoundary.Initialize(ballHitBottomPublisher);
    }
}