using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    [SerializeField] string activeScene;
    AsyncOperation load;
    AsyncOperation unload;
    [SerializeField] GameObject player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        activeScene = "Village";
    }

    public void ExecuteTransition(string dest) 
    {
        StartCoroutine(Transition(dest));
        player.GetComponent<NavMeshAgent>().enabled = false;
        switch (dest) 
        {
            case "Forest":
                player.transform.position = new Vector3(-2, 1.2f, -4);
                break;
            case "Village":
                player.transform.position = new Vector3(-56, 1.2f, -25.5f);
                break;
            case "Bridges":
                player.transform.position = new Vector3(-2, 1.2f, -4);
                break;
            case "Garden":
                break;
            case "Castle":
                break;
            default:
                Debug.LogError("Could not catch scene to set position");
                break;
        }
        player.GetComponent<NavMeshAgent>().enabled = true;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(dest));
    }

    public IEnumerator Transition(string dest) 
    {
        SwitchScenes(dest);
        while (load.isDone == false & unload.isDone == false)
        {
            yield return new WaitForSeconds(0.4f);
        }
        load = null;
        unload = null;
        //player.transform.position = Vector3.zero;
    }

    public void SwitchScenes(string dest) 
    {
        load = SceneManager.LoadSceneAsync(dest, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(activeScene);
        activeScene = dest;
    }
}
