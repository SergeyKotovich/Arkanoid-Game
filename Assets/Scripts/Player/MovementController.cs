using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        private float _speed;
        private IInputHandler _inputHandler;
        private BoundaryPointsProvider _boundaryPointsProvider;
        private bool _isUsingKeyboard;
        private bool _isUsingMouse;

        public void Initialize(IInputHandler inputHandler, BoundaryPointsProvider boundaryPointsProvider, float speed)
        {
            _inputHandler = inputHandler;
            _boundaryPointsProvider = boundaryPointsProvider;
            _speed = speed;
            _isUsingKeyboard = true;
        }

        private void Update()
        {
            if (_isUsingKeyboard)
            {
                var input = _inputHandler.GetKeyboardInput();
                MoveWithKeyboard(input);
            }

            if (_isUsingMouse)
            {
                var mousePosition = _inputHandler.GetMousePosition();
                MoveWithMouse(mousePosition);
            }
        }

        [UsedImplicitly]
        public void SetActiveInput()
        {
            _isUsingKeyboard = !_isUsingKeyboard;
            _isUsingMouse = !_isUsingMouse;
        }
        
        public void StopMovement()
        {
            _isUsingKeyboard = false;
            _isUsingMouse = false;
        }

        private void MoveWithKeyboard(Vector2 direction)
        {
            var movement = new Vector3(direction.x, 0, 0) * _speed * Time.deltaTime;
            var newPosition = transform.position + movement;

            newPosition.x = Mathf.Clamp(newPosition.x, _boundaryPointsProvider.LeftBoundary.position.x,
                _boundaryPointsProvider.RightBoundary.position.x);

            transform.position = newPosition;
        }

        private void MoveWithMouse(Vector3 mousePosition)
        {
            float clampedX = Mathf.Clamp(mousePosition.x, _boundaryPointsProvider.LeftBoundary.position.x,
                _boundaryPointsProvider.RightBoundary.position.x);
            var newPosition = new Vector3(clampedX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, _speed * Time.deltaTime);
        }
    }
}