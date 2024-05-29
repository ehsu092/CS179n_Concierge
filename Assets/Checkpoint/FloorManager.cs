using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Required for using Coroutines


public class FloorManager : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;
 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");

            playerg.SetActive(false); // Disable the player GameObject temporarily
            player.position = destination.position; // Teleport the player to the destination
            playerg.SetActive(true); // Re-enable the player GameObject
        }
    }
}
