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
        private IDisposable _ballHitBottomSubscriber;


        [Inject]
        public void Construct(BallConfig ballConfig, IPlayerPositionProvider playerPositionProvider, ISubscriber<BallHitBottomMessage> ballHitBottomSubscriber)
        {
            _ballHitBottomSubscriber = ballHitBottomSubscriber.Subscribe(_ => SetBallPosition());
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

            if (Input.GetMouseButtonDown(0))
            {
                LaunchBall();
            }
        }

        private void SetBallPosition()
        {
            var playerPosition = _playerPositionProvider.PlayerPosition;
            playerPosition.y += _shift;
            transform.position = playerPosition;
            _isMoving = false;
        }

        private void LaunchBall()
        {
            _rigidbody.AddForce(Vector2.up * _power);
            _isMoving = true;
        }
    }
}