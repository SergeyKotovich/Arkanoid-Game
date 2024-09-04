using UnityEngine;
using VContainer;

namespace Player
{
    public class Player : MonoBehaviour, IPlayerPositionProvider
    {
        public Vector3 PlayerPosition => transform.position;

        [SerializeField] private MovementController _movementController;

        [Inject]
        public void Construct(IInputHandler inputHandler, Boundary boundary, PlayerConfig playerConfig)
        {
            _movementController.Initialize(inputHandler, boundary.BoundaryPointsProvider, playerConfig.Speed);
        }
    }
}