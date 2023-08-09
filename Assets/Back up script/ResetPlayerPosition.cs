using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    public Transform respawnPoint; // The point where the player should respawn

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dez New"))
        {
            ResetPosition(other.gameObject);
        }
    }

    private void ResetPosition(GameObject player)
    {
        player.transform.position = respawnPoint.position;
    }
}
