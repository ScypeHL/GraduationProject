using TMPro;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [HideInInspector] public bool isInteracting = false;
    private int index = 0;
    
    private void Start()
    {
        text = GameManager.instance.GetComponent<UIPanelManager>().textBubble.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "";
    }

    private void Update()
    {
        if (isInteracting == true & Input.GetMouseButtonDown(0)) { Story(); index++; }
    }

    public void Story()
    {
        switch (index)
        {
            case 0:
                text.text = "This is the first message";
                break;
            case 1:
                text.text = "Who cares about you anyway";
                break;
            case 2:
                text.text = "Erm actually it is 5";
                break;
            default:
                GameManager.instance.GetComponent<UIPanelManager>().OpenTextBubble();
                index = 0;
                isInteracting = false;
                break;
        }
    }

}
