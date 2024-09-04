using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Create PlayerConfig", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}