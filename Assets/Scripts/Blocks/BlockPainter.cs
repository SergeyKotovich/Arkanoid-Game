using UnityEngine;
using UnityEngine.UI;

public class BlockPainter : MonoBehaviour
{
    [SerializeField] private Dropdown _blockTypeDropdown; 
    [SerializeField] private GameObject[] _blockPrefabs;  
    [SerializeField] private Grid _grid;                  
    [SerializeField] private Vector2 _gridSize;           
    [SerializeField] private Transform _parent;
    
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _blockTypeDropdown.onValueChanged.AddListener(OnBlockTypeChanged);  
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))  
        {
            var mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var gridPos = _grid.WorldToCell(mouseWorldPos); 
            var blockPosition = _grid.GetCellCenterWorld(gridPos);  
            
            var block = Physics2D.OverlapBox(blockPosition, _gridSize, 0);
            if (block == null)
            {
                SpawnBlock(blockPosition);
            }
            else
            {
                Debug.Log("Block already exists at this position.");
            }
        }
    }

    private void OnBlockTypeChanged(int index)
    {
        Debug.Log("Selected block type: " + _blockTypeDropdown.options[index].text);
    }

    private void SpawnBlock(Vector3 position)
    {
        var selectedBlockIndex = _blockTypeDropdown.value;  
        var blockPrefab = _blockPrefabs[selectedBlockIndex];  
        Instantiate(blockPrefab, position, Quaternion.identity, _parent);  
    }
}
