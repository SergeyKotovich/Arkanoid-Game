using UnityEngine;
using UnityEngine.UI;

public class BlockPainter : MonoBehaviour
{
    [SerializeField] private Dropdown _blockTypeDropdown;  // Dropdown для выбора типа блока
    [SerializeField] private GameObject[] _blockPrefabs;   // Массив префабов для выбора
    [SerializeField] private Grid _grid;                  // Grid для привязки по ячейкам
    [SerializeField] private Vector2 _gridSize;           // Размер клетки грида для проверки коллизий
    [SerializeField] private Transform _parent;
    
    private Camera _mainCamera;
    
    void Start()
    {
        _mainCamera = Camera.main;
        _blockTypeDropdown.onValueChanged.AddListener(OnBlockTypeChanged);  // Подписываемся на событие изменения выбранного блока
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))  // Проверяем клик мыши
        {
            Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = _grid.WorldToCell(mouseWorldPos);  // Привязываем позицию мыши к сетке
            Vector3 blockPosition = _grid.GetCellCenterWorld(gridPos);  // Определяем центр клетки сетки

            // Проверка: есть ли блок в этом месте?
            Collider2D collider = Physics2D.OverlapBox(blockPosition, _gridSize, 0);
            if (collider == null)
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
        // Логика для изменения текущего префаба (если необходимо), можно хранить выбранный индекс
        Debug.Log("Selected block type: " + _blockTypeDropdown.options[index].text);
    }

    private void SpawnBlock(Vector3 position)
    {
        int selectedBlockIndex = _blockTypeDropdown.value;  // Получаем выбранный блок из Dropdown
        GameObject blockPrefab = _blockPrefabs[selectedBlockIndex];  // Получаем префаб по индексу
        Instantiate(blockPrefab, position, Quaternion.identity, _parent);  // Спавним блок
    }
}
