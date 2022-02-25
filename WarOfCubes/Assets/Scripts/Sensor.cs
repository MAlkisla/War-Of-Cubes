using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block") || other.CompareTag("UpGround"))       //If the gameobject collides with a "Block" or with an "UpGround"
            Destroy(gameObject);        //Then this gameobject gets destroyed

        if (other.CompareTag("DownGround"))      //If the gameobject collides with a "DownGround"
        {
            if (other.GetComponent<DownGround>().empty)      //If the floor is empty
                other.GetComponent<DownGround>().empty = false;      //It won't be empty anymore
            else
                Destroy(gameObject);        //If the floor is not empty then this gameobject gets destroyed
        }
    }
}
