using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this script done by Adrian Wisniewski sole purpose is to add into 2 lists GameObjects' transforms
 * located under the "SpawnPoints" GameObject in the Hierarchy.
 */

public class spawnPosition : MonoBehaviour
{
    public List<GameObject> charactersSpawnPoint = new List<GameObject>();
    public List<GameObject> EnemiesSpawnPoint = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
