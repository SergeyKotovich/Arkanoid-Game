using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonPositionController : MonoBehaviour
{
    [SerializeField] private Button _changeControls;

    [UsedImplicitly]
    public void SetParent(Transform parent)
    {
        _changeControls.transform.SetParent(parent);
        _changeControls.transform.localPosition = Vector3.zero;
    }
}