using System;
using EventMessages;
using MessagePipe;
using UnityEngine;

namespace Ball
{
    public class BallCollisionHandler : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _power;

        public void Initialize(float power)
        {
            _power = power;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                var bounceDirection = new Vector2(
                 (transform.position.x - col.transform.position.x) / col.collider.bounds.size.x,
                   1).normalized;
               _rigidbody.velocity = bounceDirection * _power;
            }
        }
    }
}