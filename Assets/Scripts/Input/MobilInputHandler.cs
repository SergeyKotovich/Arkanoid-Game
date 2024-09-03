using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MobilInputHandler : IInputHandler
{
    private Button _leftButton;
    private Button _rightButton;

    private bool _isLeftPressed;
    private bool _isRightPressed;

    [Inject]
    public void Construct(Button leftButton, Button rightButton)
    {
        _leftButton = leftButton;
        _rightButton = rightButton;

        _leftButton.onClick.AddListener(OnLeftButtonPressed);
        _rightButton.onClick.AddListener(OnRightButtonPressed);
    }

    private void OnLeftButtonPressed()
    {
        _isLeftPressed = true;
        _isRightPressed = false;
    }
    
    private void OnRightButtonPressed()
    {
        _isRightPressed = true;
        _isLeftPressed = false;
    }

    public Vector2 GetInput()
    {
        Vector2 input = Vector2.zero;

        if (_isLeftPressed)
        {
            input.x = -1;
        }
        else if (_isRightPressed)
        {
            input.x = 1;
        }

        return input;
    }
    
    public void ResetInput()
    {
        _isLeftPressed = false;
        _isRightPressed = false;
    }
}