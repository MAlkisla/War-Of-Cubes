using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownGround : MonoBehaviour
{
    [HideInInspector]
    public bool empty = true;

    private void OnMouseDown()      //If the gameobject is clicked
    {
        if (!FindObjectOfType<GameManager>().canSpawn) //value(canSpawn) == false
        {
            FindObjectOfType<GameManager>().canSpawn = true;

            //Makes the players/enemies spawn their blocks
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Block"))
                obj.GetComponent<Block>().Invoke("Spawn", 0.25f);

            BlockPooler.Instance.SpawnFromPool("Player", transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);       //Spawns a player to the position of the clicked gameobject
        }
    }
}
