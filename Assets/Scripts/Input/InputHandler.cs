using Player;
using UnityEngine;

public class InputHandler : IInputHandler
{
    private readonly Camera _mainCamera;
    
    public InputHandler(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    public Vector2 GetKeyboardInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            return Vector2.right;
        }

        return Vector2.zero;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        
        return worldPosition;
    }
}