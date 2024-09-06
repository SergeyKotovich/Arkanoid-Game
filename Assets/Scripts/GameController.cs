using VContainer.Unity;

public class GameController : IStartable
{
    private UIController _uiController;
    private readonly BlocksSpawner _blocksSpawner;

    public GameController(UIController uiController, BlocksSpawner blocksSpawner)
    {
        _blocksSpawner = blocksSpawner;
        _uiController = uiController;
    }

    public void Start()
    {
        _blocksSpawner.StartSpawn();
    }
}