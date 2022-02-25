using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockPooler blockPooler;

    void Start()
    {
        blockPooler = BlockPooler.Instance;
        if (FindObjectOfType<GameManager>().canSpawn)
            Invoke(nameof(Spawn), 0.2f);      //Spawns block after x seconds
    }

    public void Spawn()
    {
        for (int i = 0; i < transform.parent.childCount - 1; i++)     //Loops through the children of the gameobject (the remaining sensors)
        {
            GameObject tempBlock = blockPooler.SpawnFromPool(name, transform.parent.GetChild(i).position, Quaternion.identity);        //Spawns a new block to the position of the child
            tempBlock.transform.Find(name).GetComponent<Animation>().Play(transform.parent.GetChild(i).name);        //Plays animation
        }

        Invoke(nameof(PlayBounceAnim), 0.5f);     //Starts to bounce after x seconds
        blockPooler.CountBlocks(name);      //Counts spawned blocks of this player/enemy
    }

    public void PlayBounceAnim()
    {
        GetComponent<Animation>().Play("Bounce");       //Plays bounce animation
    }
}
