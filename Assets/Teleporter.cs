using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class Teleporter : MonoBehaviour
{
    public GameObject Player;
    public GameObject TeleportTo;
    public GameObject StartTeleporter;
    private GameObject bottoms;
 
    private void OnTriggerEnter(Collider collision)
    {
        bottoms = GameObject.Find("check1");

        if (bottoms == null)
        {
            Debug.LogError("Bottoms object not found in the scene.");
        }

        if (collision.gameObject.CompareTag("Teleporter"))
        {
            Player.transform.position = TeleportTo.transform.position;
            Player.transform.rotation = TeleportTo.transform.rotation;
            // Player.transform.SetParent(TeleportTo);
        }
 
        // if (collision.gameObject.CompareTag("SecondTeleporter"))
        // {
        //     Player.transform.position = StartTeleporter.transform.position;
        // }
    }
}