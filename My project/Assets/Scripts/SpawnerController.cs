using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyToSpawn;
    private float rangeWindow;
    
    void Start()
    {
        rangeWindow = 3.5f;
        Spawn(3);
    }

    private void Spawn(int amount) 
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newEnemy = Instantiate(
                enemyToSpawn,
                new Vector3(transform.position.x + Random.Range(-rangeWindow, rangeWindow), 
                            transform.position.y, 
                            transform.position.z + Random.Range(-rangeWindow, rangeWindow)),
                Quaternion.identity);
        }
    }
}
