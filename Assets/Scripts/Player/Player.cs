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
        private IDisposable _subscriptions;

        [Inject]
        public void Construct(IInputHandler inputHandler, Boundary boundary, PlayerConfig playerConfig,
            ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber, IPublisher<GameOverMessage> gameOverPublisher,
            ISubscriber<GameOverMessage> gameOverSubscriber)
        {
            _movementController.Initialize(inputHandler, boundary.BoundaryPointsProvider, playerConfig.Speed);
            _healthController = new HealthController(playerConfig.CountLives, ballHitBottomSubscriber, gameOverPublisher);
            _subscriptions = gameOverSubscriber.Subscribe(_ => StopPlayer());
        }

        private void StopPlayer()
        {
            _movementController.StopMovement();
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}