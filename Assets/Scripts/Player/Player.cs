using System;
using EventMessages;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Player
{
    public class Player : MonoBehaviour, IPlayerPositionProvider
    {
        public Vector3 PlayerPosition => transform.position;
        public IHealthHandler HealthController => _healthController;

        [SerializeField] private MovementController _movementController;
        private HealthController _healthController;

        [Inject]
        public void Construct(IInputHandler inputHandler, Boundary boundary, PlayerConfig playerConfig,
            ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber)
        {
            _movementController.Initialize(inputHandler, boundary.BoundaryPointsProvider, playerConfig.Speed);
            _healthController = new HealthController(playerConfig.CountLives, ballHitBottomSubscriber);
        }
        
    }
}