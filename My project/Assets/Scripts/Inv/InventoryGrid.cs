using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    const float SPRITE_WIDTH = 64f;
    const float SPRITE_HEIGHT = 64f;

    int GRID_WIDTH = 10;
    int GRID_HEIGHT = 5;

    bool activationTrigger;

    RectTransform rectTransform;
    Vector2 mouseGridPos;

    [SerializeField] RectTransform highlight;
    Vector2Int selectedCell;
    InventoryItem selectedItem;
    RectTransform selectedItemRect;

    InventoryItem[,] itemMap;
    [SerializeField] List<GameObject> itemPrefab;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        highlight.SetParent(rectTransform);

        InitGrid(GRID_WIDTH, GRID_HEIGHT);
        GameObject itemTest = Instantiate(itemPrefab[0]);
        InventoryItem inventoryItemTest = itemTest.GetComponent<InventoryItem>();
        PlaceItem(inventoryItemTest, 3, 2);

        GameObject itemTest2 = Instantiate(itemPrefab[1]);
        InventoryItem inventoryItemTest2 = itemTest2.GetComponent<InventoryItem>();
        PlaceItem(inventoryItemTest2, 1, 1);
    }

    private void Update()
    {
        if (activationTrigger)
        {

            highlight.localPosition = new Vector3(
                ((int)((Input.mousePosition.x - rectTransform.position.x) / SPRITE_WIDTH)) * SPRITE_HEIGHT,
                ((int)((Input.mousePosition.y - rectTransform.position.y) / SPRITE_HEIGHT)) * SPRITE_HEIGHT, 
                highlight.localPosition.z);

            if (selectedItemRect != null) { selectedItemRect.position = Input.mousePosition; }
            if (Input.GetMouseButtonDown(0))
            {
                selectedCell = GetCellPos(Input.mousePosition);
                if (selectedItem == null)
                {
                    selectedItem = PickUpItem(selectedCell.x, selectedCell.y);
                    selectedItemRect = selectedItem.GetComponent<RectTransform>();
                }
                else
                {
                    if (itemMap[selectedCell.x, selectedCell.y] != null) { return; }
                    PlaceItem(selectedItem, selectedCell.x, selectedCell.y);
                    selectedItem = null;
                    selectedItemRect = null;
                }
            }
        }
    }

    private void InitGrid(int _width, int _height)
    {
        itemMap = new InventoryItem[_width, _height];
        Vector2 size = new Vector2();
        size.x = SPRITE_WIDTH * _width; size.y = SPRITE_HEIGHT * _height;
        rectTransform.sizeDelta = size;
    }

    public Vector2Int GetCellPos(Vector2 _mousePos)
    {
        mouseGridPos.x = _mousePos.x - rectTransform.position.x;
        mouseGridPos.y = rectTransform.position.y - _mousePos.y;

        Vector2Int cell = new Vector2Int();
        cell.x = (int) (mouseGridPos.x / SPRITE_WIDTH);
        cell.y = (int)(mouseGridPos.y / SPRITE_HEIGHT);
        return cell;
    }

    private Vector2 GetCellCoor(int column, int row)
    {
        Vector2 coor = new Vector2();
        coor.x = SPRITE_WIDTH * column + (SPRITE_WIDTH / 2);
        coor.y = -(SPRITE_HEIGHT * row + (SPRITE_HEIGHT / 2));
        return coor;
    }

    public void PlaceItem(InventoryItem item, int posx, int posy)
    {
        if (!IsWithinBoundry(posx, posy)) { return; }
        RectTransform itemRectTransform = item.GetComponent<RectTransform>();
        itemRectTransform.SetParent(transform);

        itemMap[posx, posy] = item;

        Vector2 gridPosition = new Vector2();
        gridPosition.x = SPRITE_WIDTH * posx + (SPRITE_WIDTH / 2);
        gridPosition.y = -(SPRITE_HEIGHT * posy + (SPRITE_HEIGHT / 2));
        itemRectTransform.localPosition = gridPosition;
    }

    public InventoryItem PickUpItem(int posx, int posy)
    {
        InventoryItem pickedItem = itemMap[posx, posy];
        itemMap[posx, posy] = null;
        return pickedItem;
    }

    private bool IsWithinBoundry(int x, int y)
    {
        if (x < 0 || y < 0) return false;
        if (x >= GRID_WIDTH || y >= GRID_HEIGHT) return false;
        else return true;
    }

    private void HighlightCell()
    {
    }

    public void OnPointerEnter(PointerEventData eventData){activationTrigger = true;}
    public void OnPointerExit(PointerEventData eventData){activationTrigger = false;}
}
