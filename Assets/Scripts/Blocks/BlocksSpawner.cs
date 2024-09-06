using UnityEngine;
using VContainer;

public class BlocksSpawner : MonoBehaviour
{
    [SerializeField] private Grid _gridBlocks;
    private BlocksFactory _blocksFactory;

    [Inject]
    public void Construct(BlocksFactory blocksFactory)
    {
        _blocksFactory = blocksFactory;
    }

    public void StartSpawn()
    {
        for (int x = 0; x < 12; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                var position = _gridBlocks.CellToWorld(new Vector3Int(x, y, 0));
                var block = _blocksFactory.Spawn(position);
                block.transform.SetParent(_gridBlocks.transform);
            }
        }
    }
}