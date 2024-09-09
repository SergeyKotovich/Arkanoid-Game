using JetBrains.Annotations;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    [SerializeField] private GameObject _keyboardDescription;
    [SerializeField] private GameObject _mouseDescription;
    private bool _isKeyboardActive = true;

    [UsedImplicitly]
    public void UpdateDescription()
    {
        _isKeyboardActive = !_isKeyboardActive;
        _keyboardDescription.SetActive(_isKeyboardActive);
        _mouseDescription.SetActive(!_isKeyboardActive);
    }
}