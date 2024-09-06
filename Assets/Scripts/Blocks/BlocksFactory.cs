using EventMessages;
using MessagePipe;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class BlocksFactory
{
    private readonly IObjectResolver _container;
    private readonly ObjectPool<Block> _blockPool;
    private readonly BlockConfig _blockConfig;
    private Vector3 _position;
    private IPublisher<BlockDestroyed> _blockDestroyedPublisher;

    public BlocksFactory(IObjectResolver container, BlockConfig blockConfig,
        IPublisher<BlockDestroyed> blockDestroyedPublisher)
    {
        _blockDestroyedPublisher = blockDestroyedPublisher;
        _blockConfig = blockConfig;
        _container = container;
       _blockPool = new ObjectPool<Block>(Create, Get, Release);
    }
    
    public Block Spawn(Vector3 position)
    {
        _position = position;
        var block = _blockPool.Get();
        block.Initialize(_blockConfig.AmountPoints, _blockDestroyedPublisher);
        block.transform.position = _position;
        return block;
    }

    private void Release(Block block)
    {
       block.gameObject.SetActive(false);
       block.transform.position = Vector3.zero;
       block.transform.rotation = Quaternion.identity;
    }

    private void Get(Block block)
    {
        block.gameObject.SetActive(true);
    }

    private Block Create()
    {
        return _container.Instantiate(_blockConfig.BlockPrefab);
    }
}