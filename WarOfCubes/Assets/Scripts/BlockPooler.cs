using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockPooler : MonoBehaviour
{
    public GameObject scoreText;

    private int floorCount, blockCount = 0;

    [System.Serializable]
    public class Pool       //Nested classes can directly use names, type names, names of static members, and enumerators only from the enclosing class.
    {
        public string name;
        public GameObject prefab;
        public int size;

        [HideInInspector]
        public int count = 0;
        [HideInInspector]
        public GameObject tempObj;
    }

    public static BlockPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); //Creates new pool queue

            //Rotates the objects in the list, creates them, disables their visibility and adds them to the object pool queue.
            for (int i = 0; i < pool.size; i++) // 
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.name, objectPool); //Adds to the repository library according to name and queue.
        }

        Invoke(nameof(CountFloors), 0.6f);
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(name)) // If there is no object with this name in the library
        {
            Debug.LogWarning("Pool with name " + name + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[name].Dequeue(); // Remove the created block from the queue, activate it and assign its position

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[name].Enqueue(objectToSpawn);

        //Returns the pool. If the name of the block in the pool is the same as the created block, it identifies a temporary object.
        for (int i = 0; i < pools.Count; i++) //
        {
            if (pools[i].name == name)
                pools[i].tempObj = objectToSpawn;
        }

        return objectToSpawn;
    }

    public void CountFloors()
    {
        floorCount = GameObject.FindGameObjectsWithTag("DownGround").Length;
    }

    public void CountBlocks(string objName)
    {
        //Counts the blocks of each player
        foreach (Pool pool in pools)
        {
            if (pool.name == objName)
                pool.count++;
        }

        blockCount++;

        if (blockCount == floorCount)      //If there are no more empty floors
        {
            bool playerWins = true;
            int playerPoolIndex = GetPlayerPoolIndex();

            for (int i = 0; i < pools.Count; i++)
            {
                //Counts the score of each players and writes it out
                GameObject tempScoreText = Instantiate(scoreText, pools[i].tempObj.transform.position, Quaternion.identity);
                tempScoreText.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().color = Color.white;
                tempScoreText.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ((float)pools[i].count / (float)floorCount * 100f).ToString("0") + "%";

                //If the score of the enemy is more than the score of the player, then the game is over
                if (i != playerPoolIndex)
                {
                    if (pools[i].count > pools[playerPoolIndex].count)
                        playerWins = false;
                }
            }

            if (playerWins)
                FindObjectOfType<GameManager>().LevelCompletedActivation();
            else
                FindObjectOfType<GameManager>().GameOverPanelActivation();
        }
    }

    public int GetPlayerPoolIndex()
    {
        int index = 0;
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].name == "Player")
                index = i;
        }
        return index;
    }
}
