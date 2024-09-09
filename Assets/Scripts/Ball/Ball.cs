using System;
using EventMessages;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallCollisionHandler _ballCollisionHandler;
        private float _power;
        private float _shift;
        private bool _isMoving;

        private Rigidbody2D _rigidbody;

        private IPlayerPositionProvider _playerPositionProvider;
        private IDisposable _subscriptions;

        [Inject]
        public void Construct(BallConfig ballConfig, IPlayerPositionProvider playerPositionProvider,
            ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber, ISubscriber<GameOverMessage> gameOverSubscriber,
            ISubscriber<LevelFinished> levelFinishedSubscriber)
        {
            _subscriptions = DisposableBag.Create(ballHitBottomSubscriber.Subscribe(_ => ResetBall()),
                gameOverSubscriber.Subscribe(_ => DeactivateBall()),
                levelFinishedSubscriber.Subscribe(_ => ResetBall()));

            _playerPositionProvider = playerPositionProvider;
            _power = ballConfig.Power;
            _shift = ballConfig.Shift;
            _rigidbody = GetComponent<Rigidbody2D>();
            _ballCollisionHandler.Initialize(_power);
        }

        private void LateUpdate()
        {
            if (_isMoving)
            {
                return;
            }
            
            SetBallPosition();

            if (Input.GetMouseButtonDown(1))
            {
                LaunchBall();
            }
        }
        
        private void ResetBall()
        {
            _isMoving = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;

            SetBallPosition();
        }

        private void DeactivateBall()
        {
            gameObject.SetActive(false);
        }

        private void SetBallPosition()
        {
            var playerPosition = _playerPositionProvider.PlayerPosition;
            playerPosition.y += _shift;
            transform.position = playerPosition;
        }

        private void LaunchBall()
        {
            _rigidbody.velocity = Vector2.up * _power;
            _isMoving = true;
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}