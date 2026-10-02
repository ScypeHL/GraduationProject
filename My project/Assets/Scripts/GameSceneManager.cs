using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    [SerializeField] string activeScene;
    AsyncOperation load;
    AsyncOperation unload;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name != "Essential")
            {
                activeScene = SceneManager.GetSceneAt(i).name;
                break;
            }
        }
    }

    public void ExecuteTransition(string dest) 
    {
        StartCoroutine(Transition(dest));
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
    }

    public void SwitchScenes(string dest) 
    {
        load = SceneManager.LoadSceneAsync(dest, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(activeScene);
        activeScene = dest;
    }
}
