using UnityEngine;

namespace Ball
{
    [CreateAssetMenu(menuName = "Create BallConfig", fileName = "BallConfig", order = 0)]
    public class BallConfig : ScriptableObject
    {
        [field: SerializeField] public float Power { get; private set; }
    }
}