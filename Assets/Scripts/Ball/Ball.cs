using System;
using EventMessages;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Ball
{
    public class Ball : MonoBehaviour
    {
        private float _power;
        private const float _shift = 0.35f;
        private bool _isMoving;

        private Rigidbody2D _rigidbody;

        private IPlayerPositionProvider _playerPositionProvider;
        private IDisposable _subscriptions;

        [Inject]
        public void Construct(BallConfig ballConfig, IPlayerPositionProvider playerPositionProvider,
            ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber, ISubscriber<GameOverMessage> gameOverSubscriber)
        {
            _subscriptions = DisposableBag.Create(ballHitBottomSubscriber.Subscribe(_ => ResetBall()),
                gameOverSubscriber.Subscribe(_ => DeactivateBall()));
            
            _playerPositionProvider = playerPositionProvider;
            _power = ballConfig.Power;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            if (_isMoving)
            {
                return;
            }

            SetBallPosition();

            if (Input.GetKeyDown(KeyCode.Space))
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
            _rigidbody.AddRelativeForce(Vector2.up * _power);
            _isMoving = true;
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}