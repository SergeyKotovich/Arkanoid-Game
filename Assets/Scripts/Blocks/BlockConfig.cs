using UnityEngine;

[CreateAssetMenu(menuName = "Create BlockConfig", fileName = "BlockConfig", order = 0)]
public class BlockConfig : ScriptableObject
{
    [field: SerializeField] public Block BlockPrefab { get; private set; }
    [field: SerializeField] public int AmountPoints { get; private set; }
    [field: SerializeField] public ExtraLife ExtraLifePrefab { get; private set; }
}