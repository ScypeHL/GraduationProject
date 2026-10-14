using TMPro;
using UnityEngine;


public class UIPanelManager : MonoBehaviour
{
    [SerializeField] GameObject invPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject statPanel;
    [SerializeField] GameObject textBubble;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) { OpenInventoryPanel(); }
        if (Input.GetKeyDown(KeyCode.K)) { OpenStatPanel(); }
        if (Input.GetKeyDown(KeyCode.L)) { OpenQuestPanel(); }
        if (Input.GetKeyDown(KeyCode.N)) { OpenTextBubble(); }
    }

    public void OpenInventoryPanel() { invPanel.SetActive(!invPanel.activeInHierarchy); questPanel.SetActive(false); }
    public void OpenStatPanel() { statPanel.SetActive(!statPanel.activeInHierarchy); }
    public void OpenQuestPanel() { questPanel.SetActive(!questPanel.activeInHierarchy); invPanel.SetActive(false); }
    public void OpenTextBubble() { textBubble.SetActive(!textBubble.activeInHierarchy); }
}
