using System.Collections;
using System.Collections.Generic;
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
    
    Vector2Int selectedCell;
    InventoryItem selectedItem;

    InventoryItem[,] itemMap;

    [SerializeField] GameObject itemPrefab;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        InitGrid(GRID_WIDTH, GRID_HEIGHT);
        GameObject itemTest = Instantiate(itemPrefab);
        InventoryItem inventoryItemTest = itemTest.GetComponent<InventoryItem>();
        PlaceItem(inventoryItemTest, 3, 2);

        GameObject itemTest2 = Instantiate(itemPrefab);
        InventoryItem inventoryItemTest2 = itemTest2.GetComponent<InventoryItem>();
        PlaceItem(inventoryItemTest2, 1, 1);
    }

    private void Update()
    {
        if (activationTrigger)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectedCell = GetCellPos(Input.mousePosition);
                if (selectedItem == null)
                {
                    selectedItem = PickUpItem(selectedCell.x, selectedCell.y);
                }
                else
                {
                    PlaceItem(selectedItem, selectedCell.x, selectedCell.y);
                    selectedItem = null;
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

    public void PlaceItem(InventoryItem item, int posx, int posy)
    {
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

    public void OnPointerEnter(PointerEventData eventData){activationTrigger = true;}
    public void OnPointerExit(PointerEventData eventData){activationTrigger = false;}
}
