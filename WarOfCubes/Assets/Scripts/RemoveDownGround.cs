using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDownGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DownGround"))
        {
            Destroy(other.gameObject);      //Destroys the floor under the Obstacle
            Destroy(gameObject, 0.1f);
        }
    }
}
