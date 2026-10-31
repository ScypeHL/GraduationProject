using TMPro;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using Unity.VisualScripting.FullSerializer;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text; 
    [HideInInspector] public bool isInteracting = false;
    public int index = 0;

    [SerializeField] string path;
    private string file;
    private string[] tokens;


    public string get_dialogue(int storyID)
    {
        return tokens[storyID];
    }

    private void Start()
    {
        file = File.ReadAllText(path);
        tokens = file.Split('@');
        text = GameManager.instance.GetComponent<UIPanelManager>().textBubble.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "";
    }

    private void Update()
    {
        if (isInteracting == true && Input.GetMouseButtonDown(0)) { Story(); index++; }
    }

    public void Story()
    {
        if (index < tokens.Length)
        {
            text.text = tokens[index];
        }
        else
        {
            GameManager.instance.GetComponent<UIPanelManager>().OpenTextBubble();
            index = 0;
            isInteracting = false;
        }
    }

}
