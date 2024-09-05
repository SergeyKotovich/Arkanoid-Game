using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private Image _keyboard;
    [SerializeField] private Image _mouse;
    [SerializeField] private Material _outline;

    private bool _isKeyboardSelected = true;

    private void Awake()
    {
        UpdateOutline();
    }

    [UsedImplicitly]
    public void ToggleOutline()
    {
        _isKeyboardSelected = !_isKeyboardSelected;
        
        UpdateOutline();
    }

    private void UpdateOutline()
    {
        if (_isKeyboardSelected)
        {
            _keyboard.material = _outline;
            _mouse.material = null;
        }
        else
        {
            _keyboard.material = null;
            _mouse.material = _outline;
        }
    }
}
