using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Quest
{
    public bool isActive = false;
    public bool isDone = false;
    public string title;
    public string description;
    public int rewardXP;
    public Quest dependency;
    public EndCondition endCondition;

    public string targetName;
    public int killCount;
    public int killRequired;

    GameObject itemRequired;

    public enum EndCondition
    { 
        INTERACT,
        KILL
    }

    public Quest(string _title, string _desc, int rewXP, string _target, int _killRequired, Quest depend)
    {
        title = _title;
        description = _desc;
        rewardXP = rewXP;
        targetName = _target;
        killRequired = _killRequired;
        dependency = depend;
        endCondition = EndCondition.KILL;
    }

    public Quest(string _title, string _desc, int rewXP, Quest depend)
    {
        title = _title;
        description = _desc;
        rewardXP = rewXP;
        dependency = depend;
        endCondition = EndCondition.INTERACT;
    }
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI state;

    public static QuestManager instance;

    List<Quest> arc1quests;

    private void Start()
    {
        state.text = "";
        arc1quests = new List<Quest>();
        instance = this;

        arc1quests.Add(new Quest(
            "Searching the Hero!", 
            "Look for the Hero", 
            120,
            null));
       
        arc1quests.Add(new Quest(
            "Tutorial", 
            "Walk around the village an talk to people!", 
            20,
            arc1quests[0]));

        arc1quests.Add(new Quest(
            "Kill Bee All", 
            "Bees are giving headaches to people. Get rid of them!", 
            20, 
            "Bee",
            3,
            arc1quests[1]));

        arc1quests.Add(new Quest(
            "Bees Gone", 
            "Report the bees to villager!", 
            100,
            arc1quests[2]));

        arc1quests.Add(new Quest(
            "Search Continues", 
            "Look for the hero!", 
            20,
            arc1quests[1]));

        arc1quests[0].isActive = true;
    }

    private void FixedUpdate()
    {
        title.text = "";
        description.text = "";
        state.text = "";
        for (int i = 0; i < arc1quests.Count; i++) 
        {
            if (arc1quests[i].isActive)
            {
                title.text = arc1quests[i].title;
                description.text = arc1quests[i].description;
                if (arc1quests[i].killRequired > 0)
                {
                    string temp;
                    temp = arc1quests[i].killCount.ToString();
                    temp += " / ";
                    temp = temp + arc1quests[i].killRequired.ToString();
                    state.text = temp;
                }
            }
        }
    }

    /*
    void node_new(Quest head, string _title, string _desc, int rewXP)
    {
        Quest curr;
        curr = head;
        while (curr != null) 
        {
            curr = curr.next;
        };
        print(arc1Head); print(arc1Head.next);
        curr.next = new Quest(_title, _desc, rewXP);
    }*/

    public void deposit_kill(string name) 
    {
        for (int i = 0; i < arc1quests.Count; i++) 
        {
            if (arc1quests[i].targetName == name) 
            { 
                print(arc1quests[i].title); 
                arc1quests[i].killCount++;
                if (arc1quests[i].killCount >= arc1quests[i].killRequired)
                {
                    arc1quests[i].isDone = true;
                    arc1quests[i].isActive = false;

                    for (int h = 0; h < arc1quests.Count; h++)
                    {
                        if (arc1quests[h].dependency == arc1quests[i])
                        {
                            //print(arc1quests[i].title + " - " +arc1quests[h].title);
                            arc1quests[h].isActive = true;
                            return;
                        }
                    }
                }
            }
        }
    }

    public void end_quest(int quest) 
    {
        if (arc1quests[quest].isDone) return;
        if (arc1quests[quest].isActive) 
        {
            if (arc1quests[quest].endCondition == Quest.EndCondition.INTERACT) 
            {
                arc1quests[quest].isActive = false;
                arc1quests[quest].isDone = true;

                for (int i = 0; i < arc1quests.Count; i++)
                {
                    if (arc1quests[i].dependency == arc1quests[quest])
                    {
                        arc1quests[i].dependency.isActive = true;
                    }
                }
            }
        }
        if (arc1quests[quest].dependency.isActive) 
        {
            arc1quests[quest].dependency.isActive = false;
            arc1quests[quest].dependency.isDone = true;
            arc1quests[quest].isActive = true;
            reward_player(arc1quests[quest].dependency);
        }
    }

    public void reward_player(Quest quest) 
    {
        player.GetComponent<Character>().add_xp(quest.rewardXP);
    }
}
