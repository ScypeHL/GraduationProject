using TMPro;
using UnityEngine;


public class UIPanelManager : MonoBehaviour
{
    [SerializeField] GameObject invPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject statPanel;
    [SerializeField] GameObject rightClickMenu;
    public GameObject textBubble;
    [SerializeField] InventoryGrid invGrid;
    public static UIPanelManager instance;

    private void Start()
    {
        instance = this;
        PlayerInputs.instance.onRightClick += OpenRightClickMenu;
        rightClickMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) { OpenInventoryPanel(); }
        if (Input.GetKeyDown(KeyCode.K)) { OpenStatPanel(); }
        if (Input.GetKeyDown(KeyCode.L)) { OpenQuestPanel(); }
    }

    public void OpenInventoryPanel() { invPanel.SetActive(!invPanel.activeInHierarchy); questPanel.SetActive(false); }
    public void OpenStatPanel() { statPanel.SetActive(!statPanel.activeInHierarchy); }
    public void OpenQuestPanel() { questPanel.SetActive(!questPanel.activeInHierarchy); invPanel.SetActive(false); }
    public void OpenTextBubble() { textBubble.SetActive(!textBubble.activeInHierarchy); }
    
    public void OpenRightClickMenu() 
    {
        if (invGrid.state == InventoryGridState.DEFAULT)
        {
            if (invGrid.is_there_item(Input.mousePosition))
            {
                invGrid.state = InventoryGridState.RIGHT_CLICK;
                rightClickMenu.transform.position = new Vector3(Input.mousePosition.x + 96, Input.mousePosition.y - 24, 0);
                rightClickMenu.SetActive(true);
            }
        }
        else { invGrid.state = InventoryGridState.DEFAULT; rightClickMenu.SetActive(false); }
    }
}
